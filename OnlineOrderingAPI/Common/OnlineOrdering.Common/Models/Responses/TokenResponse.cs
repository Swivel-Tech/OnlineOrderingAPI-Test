using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace OnlineOrdering.Common.Models.Responses
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
