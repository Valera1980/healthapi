using System;
using System.ComponentModel.DataAnnotations.Schema;

public class BodyData {
    public int Id { get;  set;}
    public DateTime DateCreation { get;  set;}
    public DateTime DateUpdate { get;  set;}
    public double Weight { get;  set;}
    public int Waist { get;  set;}
    [ForeignKey("User")]
    public int UserId { get; set;}
}