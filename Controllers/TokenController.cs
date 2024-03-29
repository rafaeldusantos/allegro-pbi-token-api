using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using allegro_pbi_token_api.ViewModel;
using Microsoft.AspNetCore.Cors;

namespace allegro_pbi_token_api.Controllers
{
    [EnableCors("AllowAll")]
    [Route("v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        public TokenController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        // Get v1/token/generate
        [HttpGet("Generate/{id}")]
        public async Task<ActionResult> GetGenerateAsync(string id)
        {
          Organization organization = await _organizationRepository.GetOrganization(id);
          if (organization == null)
            return NotFound(new ApiResponse(404, "Organization not found"));
          using (HttpClient httpClient = new HttpClient())
          {
            string tokenEndpoint = "https://login.microsoftonline.com/common/oauth2/token";
            string accept = "application/json";

             httpClient.DefaultRequestHeaders.Add("Accept", accept);
            string postBody = null;

            postBody = $@"resource=https%3A%2F%2Fanalysis.windows.net/powerbi/api
                            &client_id={organization.pbi.clientId}
                            &grant_type=password
                            &username={organization.pbi.userName}
                            &password={Base64Decode(organization.pbi.password)}
                            &scope=openid";
            Reports report = organization.pbi.reports;
            var tokenResult = await httpClient.PostAsync(tokenEndpoint, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded"));
            tokenResult.EnsureSuccessStatusCode();
            var tokenData = await tokenResult.Content.ReadAsStringAsync();

            JObject parsedTokenData = JObject.Parse(tokenData);
            GenerateTokenRes generateTokenRes = new GenerateTokenRes(){
              AccessToken = parsedTokenData["access_token"].Value<string>(),
              GroupId = report.groupId,
              ReportId = report.reportId
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