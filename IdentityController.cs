using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[Route("identity")]
// [Authorize]
public class IdentityController : ControllerBase
{
    private readonly IConfiguration _conf;
    public IdentityController(IConfiguration conf)
    {
      _conf = conf;
    }
    [HttpGet]
    public async Task<string>  Get()
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
        {
            Address = _conf.GetValue<string>("IdentityServer:url"),
            Policy = { RequireHttps = false}
        });


        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,

            ClientId = "client",
            ClientSecret = "secret",
            Scope = "api1"
        });

        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            // return;
        }

        Console.WriteLine(tokenResponse.Json);

        // return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        return tokenResponse.Raw;
    }
}