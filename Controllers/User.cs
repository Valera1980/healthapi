using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class UserController : ControllerBase {
   AppMainContext _ctx;
   public UserController(AppMainContext _ctx){
     this._ctx = _ctx;
   }

   [HttpGet]
   [Route("user/{id}")]
   public async Task<ActionResult<User>> getUser(int id){
       var user = await this._ctx.Users.FindAsync(id);
       return Ok(user);
   }
}