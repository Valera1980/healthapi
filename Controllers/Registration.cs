using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly String _urlRegister;
    public RegistrationController(IConfiguration configuration)
    {
        _configuration = configuration;
        _urlRegister = _configuration.GetValue<string>("IdentityServer:url") + "/register";
    }
    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Post([FromBody] ModelRegistrationData m)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(m);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(_urlRegister, data);
            var rawContent = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                ModelRegistrationData modelRegistration = _deserialize(rawContent);
                return Ok(modelRegistration.Email);
            }
            else
            {
                throw new Exception(res.Content.ReadAsStringAsync().Result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.Substring(0, 90));
        }

    }
    private ModelRegistrationData _deserialize(string rawContent)
    {
        try
        {
            var regData = JsonConvert.DeserializeObject<ModelRegistrationData>(rawContent);
            return regData;
        }
        catch (SystemException ex)
        {
            throw new Exception(ex.Message);
        }

    }
}