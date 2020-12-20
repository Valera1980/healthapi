using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public interface IUserService
{
    List<User> users { get; set; }
    Task<List<User>> queryUsers();
    User findUserByEmail(string email);
}

public class UsersService : IUserService
{
    public List<User> users { get; set; }
    private readonly IConfiguration _conf;

    public UsersService(IConfiguration conf)
    {
        _conf = conf;
    }

    public async Task<List<User>> queryUsers()
    {
        HttpClient httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(_conf.GetValue<string>("IdentityServer:usersUrl"));
            var rawContent = await response.Content.ReadAsStringAsync();
            return _deserialize(rawContent);
        }
        catch (HttpRequestException ex)
        {
            System.Console.WriteLine(" === Exception === ", ex.Message);
            return new List<User> { };
        }

    }
    private List<User> _deserialize(string rawContent)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

        };
        try
        {
            List<User> users = JsonSerializer.Deserialize<List<User>>(rawContent, options);
            return users;
        }
        catch (SystemException ex)
        {
            throw ex;
        }

    }
    public User findUserByEmail(string email)
    {
        if (email.Length == 0)
        {
            throw new ArgumentException("Wrong argument email");
        }
        return this.users.Find(u => u.Email == email);
    }
}