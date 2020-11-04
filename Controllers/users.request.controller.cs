using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class UsersHttpRequest
{

    public async static void queryUsers()
    {
        HttpClient httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync("http://localhost:5000/users");
            var rawContent = await response.Content.ReadAsStringAsync();
            List<User> users = _deserialize(rawContent);
        }
        catch (HttpRequestException ex)
        {
            System.Console.WriteLine("============================");
            System.Console.WriteLine(ex.Message);
        }

    }
    private static List<User> _deserialize(string rawContent)
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
            System.Console.WriteLine("============================");
            System.Console.WriteLine(ex.Message);
            return new List<User> { };
        }

    }
}