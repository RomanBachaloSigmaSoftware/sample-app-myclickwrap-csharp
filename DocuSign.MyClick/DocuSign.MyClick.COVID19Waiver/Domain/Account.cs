using Newtonsoft.Json;

namespace DocuSign.MyClick.COVID19Waiver.Domain
{
    public class Account
    {
        [JsonProperty("account_id")]
        public string Id { get; set; }

        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("base_uri")]
        public string BaseUri { get; set; }
    }
}