using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Rest;

namespace allegro_pbi_token_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
          using (HttpClient client = new HttpClient())
          {
            string tokenEndpoint = "https://login.microsoftonline.com/common/oauth2/token";
            string accept = "application/json";
            string userName = "allegro@segmentopesquisas.com.br";
            string password = "Q2w12ssw!!";
            string clientId = "837c3d7f-b43f-4e51-a351-e9a3c8e2d64a";

            client.DefaultRequestHeaders.Add("Accept", accept);
            string postBody = null;

            postBody = $@"resource=https%3A%2F%2Fanalysis.windows.net/powerbi/api
                            &client_id={clientId}
                            &grant_type=password
                            &username={userName}
                            &password={password}
                            &scope=openid";

            var tokenResult = await client.PostAsync(tokenEndpoint, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded"));
            tokenResult.EnsureSuccessStatusCode();
            var tokenData = await tokenResult.Content.ReadAsStringAsync();

            JObject parsedTokenData = JObject.Parse(tokenData);

            return parsedTokenData["access_token"].Value<string>();
          }
        }
    }
}