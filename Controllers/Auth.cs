using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;
    public AuthController(IAuthService authServ)
    {
        this._authService = authServ;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ModelAuth authData)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
        bool isExit = this._authService.auth(authData.email, authData.password);
        if(!isExit)
        {
            return  BadRequest("Wrong password or password");
        }


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
        return Ok(new {token = tokenResponse.AccessToken});

    }
   
}