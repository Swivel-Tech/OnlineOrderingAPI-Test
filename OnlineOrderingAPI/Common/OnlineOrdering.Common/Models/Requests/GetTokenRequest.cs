﻿using Newtonsoft.Json;

namespace OnlineOrdering.Common.Models.Requests
{
    public class GetTokenRequest
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; } = "";

        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = "client_credentials";
    }
}
