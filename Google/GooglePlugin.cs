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
    [Plugin(Description = "Provides several services from Google")]
    public class GooglePlugin
    {
        private static readonly String GOOGLE_SEARCH_URL = 
            @"https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=";
        private ObservableJSONWebRequest _webRequest = new ObservableJSONWebRequest();

        public ushort MaxSearchResults { get; set; }

        public GooglePlugin()
        {
            MaxSearchResults = 3;
        }

        [Command(Description = "Searches Google with the given search query, returns the title and URL of matches.")]
        public IObservable<IEnumerable<String>> Google(IContext context, String query)
        {
            return _webRequest.Request(SearchURL(query), x => x)
                .Select(ParseSearchResults)
                ;
        }

        private String SearchURL(String query)
        {
            return GOOGLE_SEARCH_URL + HttpUtility.UrlEncode(query);
        }

        private IEnumerable<String> ParseSearchResults(JObject json)
        {
            JToken results = json["responseData"]["results"];
            if(results.IsEmpty())
                "No matches found.".AsEnumerable();

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
    }
}
