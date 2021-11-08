using System.Linq;
using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Util.JSON
{
    internal static class JsonExtensions
    {
        public static string GetKey(this JToken token)
        {
            if (token == null || token.Count() == 0)
                return "EMPTY_TOKEN";
            switch (token.Type)
            {
                case JTokenType.Object:
                    return JObject.FromObject(token).Properties().FirstOrDefault().Name;
                case JTokenType.Array:
                    return "ARRAY";
                case JTokenType.Property:
                    return (token as JProperty).Name;
                default:
                    return "NOT_DEFIENED";
            }
        }
        public static JToken ExtractFromKey(this JToken token, string key, string nestedPropKey)
        {
            return token.TryGetToken(key, out JToken keyToken) ? keyToken[nestedPropKey] : null;
        }
        public static bool TryGetToken(this JToken token, string key, out JToken tokenOut)
        {
            tokenOut = null;
            if (token != null)
            {
                switch (token.Type)
                {
                    case JTokenType.Object:
                        if (token != null)
                            tokenOut = token?[key];
                        break;
                    case JTokenType.Property:
                        if (token != null)
                            tokenOut = (token as JProperty).Value;
                        break;
                }
            }
            return tokenOut != null;
        }
        public static bool TryGetTokenIfContains(this JToken token, string[] keys, out JToken tokenOut, out string keyResult)
        {
            tokenOut = null;
            keyResult = null;
            if (token != null)
            {
                foreach (var key in keys)
                {
                    tokenOut = token[key];
                    if (tokenOut != null)
                    {
                        keyResult = key;
                        break;
                    }
                }
            }
            return tokenOut != null;
        }
    }
}