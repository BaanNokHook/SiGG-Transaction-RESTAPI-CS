using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace WEPAPI_Microservice.Settings
{

    [JsonObject("JwtSettings")]  

    public class JwtSettings
    {

        [JsonProperty("Secret")]  
        public string Secret { get; set; }


        [JsonProperty("Issuer")]  
        public string Issuer { get; set; }


        [JsonProperty("Audience")]
        public string Audience { get; set; }


        [JsonProperty("AccessException")]
        public int AccessExpiration { get; set; }


        [JsonProperty("RefreshExpiration")]

        public int RefreshExpiration { get; set; }

    
    }

    internal class JsonObjectAttribute : Attribute
    {
        private string v;

        public JsonObjectAttribute(string v)
        {
            this.v = v;
        }
    }

    internal class JsonPropertyAttribute : Attribute
    {
        private string v;

        public JsonPropertyAttribute(string v)
        {
            this.v = v;
        }
    }
}


