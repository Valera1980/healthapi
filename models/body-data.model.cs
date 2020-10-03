//    public waist: number;
//     public date_create: Date | null;
//     public date_update: Date | null;
//     public id: number | null;
//     public weight: number;
//     public user_id: number;
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