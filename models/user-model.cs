using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Password { get; set; }
    public bool Male { get; set; }
    
    public bool isEmailConfirmed { get; set; }

    public List<BodyData> BodyData { get; set; }

}