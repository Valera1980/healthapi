using HealthApi.Models;

public interface IAuthService
{
    bool auth(string email, string password);
}
public class AuthService:  IAuthService
{
    private AppMainContext _db;

    public  AuthService(AppMainContext context)
    {
        this._db = context;
    }

    public bool auth(string email, string password)
    {
        if(email == "gavrilow777@gmail.com" && password == "1111")
        {
            return true;
        }
        return false;
    }
}