using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using allegro_pbi_token_api.ViewModel;

namespace allegro_pbi_token_api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public TokenController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // Get v1/token/generate
        [HttpGet("Generate/{id}")]
        public async Task<ActionResult> GetGenerateAsync(string id)
        {
          Client client = await _clientRepository.GetClient(id);
           if (client == null)
            return new NotFoundResult();
          using (HttpClient httpClient = new HttpClient())
          {
            string tokenEndpoint = "https://login.microsoftonline.com/common/oauth2/token";
            string accept = "application/json";

             httpClient.DefaultRequestHeaders.Add("Accept", accept);
            string postBody = null;

            postBody = $@"resource=https%3A%2F%2Fanalysis.windows.net/powerbi/api
                            &client_id={client.Pbi.ClientId}
                            &grant_type=password
                            &username={client.Pbi.UserName}
                            &password={Base64Decode(client.Pbi.Password)}
                            &scope=openid";
            Reports report = client.Pbi.Reports;
            var tokenResult = await httpClient.PostAsync(tokenEndpoint, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded"));
            tokenResult.EnsureSuccessStatusCode();
            var tokenData = await tokenResult.Content.ReadAsStringAsync();

            JObject parsedTokenData = JObject.Parse(tokenData);
            GenerateTokenRes generateTokenRes = new GenerateTokenRes(){
              AccessToken = parsedTokenData["access_token"].Value<string>(),
              GroupId = report.GroupId,
              ReportId = report.ReportId
            };

            return  new ObjectResult(generateTokenRes);
          }
        }

        private static string Base64Decode(string base64EncodedData) {
          var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
          return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        } 
    }
}