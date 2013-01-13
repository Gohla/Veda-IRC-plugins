using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Web;
using Gohla.Shared.Json;
using Newtonsoft.Json.Linq;
using ReactiveIRC.Interface;
using Veda.Interface;

namespace Veda.Plugins.Google
{
    [Plugin(Name = "Google", Description = "Provides several services from Google.")]
    public class GooglePlugin
    {
        private static readonly String GOOGLE_SEARCH_URL = 
            @"https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=";
        private static readonly String GOOGLE_CALCULATOR_URL =
            @"http://www.google.com/ig/calculator?hl=en&q=";
        private ObservableJSONWebRequest _webRequest = new ObservableJSONWebRequest();

        public ushort MaxSearchResults { get; set; }

        public GooglePlugin()
        {
            MaxSearchResults = 3;
        }

        [Command(Description = "Searches Google with the given search query. Returns the title and URL of matches.")]
        public IObservable<IEnumerable<String>> Search(IContext context, String query)
        {
            return _webRequest.Request(SearchURL(query), x => x)
                .Select(ParseSearchResults)
                ;
        }

        [Command(Description = "Searches Google with the given search query. Returns the URL that would be navigated to when using the \"I'm Feeling Lucky\" button.")]
        public IObservable<String> Lucky(IContext context, String query)
        {
            return _webRequest.Request(SearchURL(query), x => x)
                .Select(ParseLuckyResults)
                ;
        }

        [Command(Description = "Evaluates given query using the Google calculator.")]
        public IObservable<String> Calculate(IContext context, String query)
        {
            return _webRequest.Request(CalculatorURL(query), x => x)
                .Select(ParseCalculatorResults)
                ;
        }

        private String SearchURL(String query)
        {
            return GOOGLE_SEARCH_URL + HttpUtility.UrlEncode(query);
        }

        private IEnumerable<String> ParseSearchResults(JObject json)
        {
            // API: https://developers.google.com/web-search/docs/#fonje
            JToken results = json["responseData"]["results"];
            if(results.IsEmpty())
                throw new InvalidOperationException("No matches found.");

            return results
                .Take(MaxSearchResults)
                .Select(ParseSearchResult)
                ;
        }

        private String ParseSearchResult(JToken json)
        {
            return ControlCodes.Bold(json["titleNoFormatting"].Value<String>())
                + ": <" + json["unescapedUrl"].Value<String>() + ">";
        }

        private String ParseLuckyResults(JObject json)
        {
            JToken results = json["responseData"]["results"];
            if(results.IsEmpty())
                throw new InvalidOperationException("No match found.");

            return ParseLuckyResults(results.First());
        }

        private String ParseLuckyResults(JToken json)
        {
            return json["unescapedUrl"].Value<String>();
        }

        private String CalculatorURL(String query)
        {
            return GOOGLE_CALCULATOR_URL + HttpUtility.UrlEncode(query);
        }

        private String ParseCalculatorResults(JToken json)
        {
            String error = json["error"].Value<String>();
            if(!String.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("Could not evaluate query.");

            String lhs = HttpUtility.HtmlDecode(json["lhs"].Value<String>());
            String rhs = HttpUtility.HtmlDecode(json["rhs"].Value<String>())
                .Replace("<sup>", "^")
                .Replace("</sup>", "")
                ;

            return lhs + " = " + rhs;
        }
    }
}
