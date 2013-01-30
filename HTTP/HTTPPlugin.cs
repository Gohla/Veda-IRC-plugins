using System;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Gohla.Shared;
using Veda.Interface;

namespace Veda.Plugins.HTTP
{
    [Plugin(Name = "HTTP")]
    public static class HTTPPlugin
    {
        private static ObservableStringWebRequest _webRequest = new ObservableStringWebRequest();
        private static Regex _titleRegex = new Regex(@"<title>\s*(.+?)\s*</title>", RegexOptions.Compiled, 
            TimeSpan.FromMilliseconds(100));

        [Command(Description = "Performs a HTTP request for given URL and returns the contents of the response.")]
        public static IObservable<String> Request(IContext context, String url)
        {
            return _webRequest.Request(url, x => x);
        }

        [Command(Description = "Performs a HTTP request for given URL and returns HTML title of the response.")]
        public static IObservable<String> Title(IContext context, String url)
        {
            return _webRequest
                .Request(url, x => x)
                .Select(GetTitle)
                ;
        }

        [Command(Description = "Encodes given text as a HTTP URL.")]
        public static String URLEncode(IContext context, String text)
        {
            return HttpUtility.UrlEncode(text);
        }

        [Command(Description = "Decodes given HTTP URL to text.")]
        public static String URLDecode(IContext context, String httpURL)
        {
            return HttpUtility.UrlDecode(httpURL);
        }

        private static String GetTitle(String html)
        {
            Match match = _titleRegex.Match(html);
            if(match.Success)
                return match.Groups[1].Value;
            else
                return String.Empty;
        }
    }
}
