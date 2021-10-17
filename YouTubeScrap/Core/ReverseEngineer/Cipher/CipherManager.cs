using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core.ReverseEngineer.Cipher.Operations;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Core.ReverseEngineer.Cipher
{
    public class CipherManager
    {
        private readonly IReadOnlyList<ICipherOperation> _operations;
        public CipherManager(JObject properties)
        {
            if (GetPlayerJavaScript(properties, out string playerScript))
                _operations = GetCipherOperations(playerScript).ToArray();
            else
                Trace.WriteLine("Could not get the player script! Deciphering will fail!");
        }
        public string DecipherAndBuildUrl(string signatureCipher)
        {
            if (_operations == null)
                return "Could not decipher the signature, no cipher operations resolved!";

            StringBuilder urlBuilder = new StringBuilder();

            int indexStart = signatureCipher.IndexOf("s=");
            int indexEnd = signatureCipher.IndexOf("&");
            string signature = signatureCipher.Substring(indexStart, indexEnd);

            indexStart = signatureCipher.IndexOf("&sp");
            indexEnd = signatureCipher.IndexOf("&url");
            string spParam = signatureCipher.Substring(indexStart, indexEnd - indexStart);

            indexStart = signatureCipher.IndexOf("&url");
            string videoUrl = signatureCipher.Substring(indexStart);


            signature = signature.Substring(signature.IndexOf("=") + 1);
            spParam = spParam.Substring(spParam.IndexOf("=") + 1);
            videoUrl = videoUrl.Substring(videoUrl.IndexOf("=") + 1);
            if (signature.IsNullEmpty())
            {
                Trace.WriteLine("Could not extract the signature");
                return null;
            }
            string signatureDeciphered = _operations.Aggregate(signature, (acc, op) => op.Decipher(acc));

            urlBuilder.Append(videoUrl);
            urlBuilder.Append($"&{spParam}=");
            urlBuilder.Append(signatureDeciphered);
            return urlBuilder.ToString();
        }
        private IEnumerable<ICipherOperation> GetCipherOperations(string playerBaseJavaScript)
        {
            string functionBody = Regex.Match(playerBaseJavaScript, @"(\w+)=function\(\w+\){(\w+)=\2\.split\(\x22{2}\);.*?return\s+\2\.join\(\x22{2}\)}").Groups[0].ToString();
            string DefinitionBody = Regex.Match(functionBody, "([\\$_\\w]+).\\w+\\(\\w+,\\d+\\);").Groups[1].Value;
            string decipherDefinition = Regex.Match(playerBaseJavaScript, $@"var\s+{DefinitionBody}=\{{(\w+:function\(\w+(,\w+)?\)\{{(.*?)\}}),?\}};", RegexOptions.Singleline).Groups[0].ToString();

            foreach (var statement in functionBody.Split(';'))
            {
                // Get the name of the function called in this statement
                var calledFuncName = Regex.Match(statement, @"\w+(?:.|\[)(\""?\w+(?:\"")?)\]?\(").Groups[1].Value;
                if (string.IsNullOrWhiteSpace(calledFuncName))
                    continue;

                if (Regex.IsMatch(decipherDefinition, $@"{Regex.Escape(calledFuncName)}:\bfunction\b\([a],b\).(\breturn\b)?.?\w+\."))
                {
                    var index = int.Parse(Regex.Match(statement, @"\(\w+,(\d+)\)").Groups[1].Value);
                    yield return new CipherSlice(index);
                }
                else if (Regex.IsMatch(decipherDefinition, $@"{Regex.Escape(calledFuncName)}:\bfunction\b\(\w+\,\w\).\bvar\b.\bc=a\b"))
                {
                    var index = int.Parse(Regex.Match(statement, @"\(\w+,(\d+)\)").Groups[1].Value);
                    yield return new CipherSwap(index);
                }
                else if (Regex.IsMatch(decipherDefinition, $@"{Regex.Escape(calledFuncName)}:\bfunction\b\(\w+\)"))
                {
                    yield return new CipherReverse();
                }
            }
        }
        private bool GetPlayerJavaScript(JObject properties, out string js)
        {
            StringBuilder player_js_url_builder = new StringBuilder();
            if (properties.TryGetToken("PLAYER_JS_URL", out JToken playerJSURL))
            {
                player_js_url_builder.Append(DataManager.NetworkData.Origin);
                player_js_url_builder.Append(playerJSURL);
            }
            /*Task<HttpResponse> playerScriptRequest = Task.Run(async () => await NetworkHandler.GetPlayerScriptAsync(player_js_url_builder.ToString()).ConfigureAwait(false));
            HttpResponse scriptResponse = playerScriptRequest.Result;
            if (!scriptResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                js = null;
                return false;
            }
            js = scriptResponse.ResponseString;*/
            js = null;
            return true;
        }
    }
}