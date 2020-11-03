using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class UsersHttpRequest
{
    // private readonly IHttpClientFactory _clientFactory;

    // public UsersHttpRequest(IHttpClientFactory clientFactory)
    // {
    //     _clientFactory = clientFactory;
    // }

    public async static void queryUsers()
    {
        HttpClient httpClient =  new HttpClient();
        var response = await httpClient.GetAsync( "http://localhost:5000/users");
        var rawContent = await response.Content.ReadAsStringAsync();

        var ii = "ttt";

        // var request = new HttpRequestMessage(HttpMethod.Get,
        //             "http://localhost:5000/users");
        // var client = _clientFactory.CreateClient();
        // var response = await client.SendAsync(request);
        //  if (response.IsSuccessStatusCode)
        // {
        //     using var responseStream = await response.Content.ReadAsStreamAsync();
        //     var users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(responseStream);
        // }
    }
}