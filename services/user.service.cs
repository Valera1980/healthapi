using System.Collections.Generic;

public class UsersService
{
    public List<User> users { get; set; }
    public UsersService(List<User> users)
    {
        this.users = users;
    }


}