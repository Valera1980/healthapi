using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("identity")]
// [Authorize]
public class IdentityController : ControllerBase
{
    [HttpGet]
    public async Task<string>  Get()
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");


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