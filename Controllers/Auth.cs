using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[Route("auth")]
public class AuthController : ControllerBase
{
    private IUserService _users;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;


    public AuthController(
        IUserService usersService,
        ILogger<AuthController> logger,
        IConfiguration configuration
        )
    {
        this._users = usersService;
        _logger = logger;
        _configuration = configuration;

    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ModelAuth authData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration.GetValue<string>("IdentityServer:url"),
                Policy = {
                    RequireHttps = false,
                }
            });
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

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
                throw new Exception(tokenResponse.Error);
            }

            List<User> users = await this._users.queryUsers();
            this._users.users = users;
            User user = this._users.findUserByEmail(authData.email);

            return Ok(new { token = tokenResponse.AccessToken, user = new ModelUserView { Name = user.Name, Id = user.Id, Email = user.Email } });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

}