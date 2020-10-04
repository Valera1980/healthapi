using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthController : ControllerBase
{
    private IUserRepository _users;

    public AuthController(IUserRepository usersRep)
    {
        this._users = usersRep;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ModelAuth authData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "client",
            GrantType = "password",
            ClientSecret = "secret",
            Scope = "api1",
            UserName = authData.email,
            Password = authData.password.ToSha256()
        });

        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            // return;
        }


        User user = await _users.getUser(authData.email);
        Console.WriteLine(tokenResponse.Json);

        // return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        return Ok(new { token = tokenResponse.AccessToken, user = new User{Name=user.Name} });

    }

}