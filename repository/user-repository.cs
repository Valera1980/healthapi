using System.Threading.Tasks;
using HealthApi.Models;
using Microsoft.EntityFrameworkCore;
public interface IUserRepository
{
    Task<User> getUser(string email);
}
public class UserRepository : IUserRepository
{
    private AppMainContext _ctx;

    public UserRepository(AppMainContext ctx)
    {
        this._ctx = ctx;
    }

    public async Task<User> getUser(string email)
    {
        var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }
    // public async Task<User> getUser(string email){
    //    var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    //    return user;
    // }
}