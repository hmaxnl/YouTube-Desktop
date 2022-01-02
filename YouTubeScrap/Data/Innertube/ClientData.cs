using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Data.Innertube
{
    public class ClientData
    {
        public ClientData(JObject clientState)
        {
            ClientState = clientState;
            SboxSettings = JsonConvert.DeserializeObject<SboxSettings>(ClientState["SBOX_SETTINGS"].ToString());
        }
        private JObject ClientState { get; }
        
        public string ApiKey => ClientState["INNERTUBE_API_KEY"]?.ToString();
        public string LoginUrl => ClientState["SIGNIN_URL"]?.ToString();
        public JObject InnertubeContext => ClientState["INNERTUBE_CONTEXT"].ToObject<JObject>();
        public SboxSettings SboxSettings { get; }
    }

    public class SboxSettings
    {
        [JsonProperty("HAS_ON_SCREEN_KEYBOARD")]
        public bool HASONSCREENKEYBOARD { get; set; }

        [JsonProperty("IS_FUSION")]
        public bool ISFUSION { get; set; }

        [JsonProperty("IS_POLYMER")]
        public bool ISPOLYMER { get; set; }

        [JsonProperty("REQUEST_DOMAIN")]
        public string REQUESTDOMAIN { get; set; }

        [JsonProperty("REQUEST_LANGUAGE")]
        public string REQUESTLANGUAGE { get; set; }

        [JsonProperty("SEND_VISITOR_DATA")]
        public bool SENDVISITORDATA { get; set; }

        [JsonProperty("SEARCHBOX_BEHAVIOR_EXPERIMENT")]
        public string SEARCHBOXBEHAVIOREXPERIMENT { get; set; }

        [JsonProperty("SEARCHBOX_ENABLE_REFINEMENT_SUGGEST")]
        public bool SEARCHBOXENABLEREFINEMENTSUGGEST { get; set; }

        [JsonProperty("SEARCHBOX_TAP_TARGET_EXPERIMENT")]
        public int SEARCHBOXTAPTARGETEXPERIMENT { get; set; }

        [JsonProperty("SEARCHBOX_ZERO_TYPING_SUGGEST_USE_REGULAR_SUGGEST")]
        public string SEARCHBOXZEROTYPINGSUGGESTUSEREGULARSUGGEST { get; set; }

        [JsonProperty("SUGG_EXP_ID")]
        public string SUGGEXPID { get; set; }

        [JsonProperty("VISITOR_DATA")]
        public string VISITORDATA { get; set; }

        [JsonProperty("SEARCHBOX_HOST_OVERRIDE")]
        public string SEARCHBOXHOSTOVERRIDE { get; set; }

        [JsonProperty("HIDE_REMOVE_LINK")]
        public bool HIDEREMOVELINK { get; set; }
    }
}