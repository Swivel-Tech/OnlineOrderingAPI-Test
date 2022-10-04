using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineOrdering.Common.Models.Requests;
using OnlineOrdering.Common.Models.Responses;
using RestSharp;

namespace OnlineOrdering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public TokenController(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }

        /// <summary>
        /// Gets the token from auth0.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<IActionResult> GetTokenFromAuth0()
        {
            var client = new RestClient("https://dev-66brtd1a.us.auth0.com/oauth/token");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("content-type", "application/json");
            var configs = new GetTokenRequest
            {
                Audience = configuration["Auth0:Audience"],
                ClientId = configuration["Auth0:ClientId"],
                ClientSecret = configuration["Auth0:ClientSecret"],
                GrantType = "client_credentials"
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(configs), ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return BadRequest();

            return Ok(JsonConvert.DeserializeObject<TokenResponse>(response.Content!));
        }
    }
}
