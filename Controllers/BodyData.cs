using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("bodydata")]
public class BodyDataController : ControllerBase
{
    private AppMainContext _ctx;

    public BodyDataController(AppMainContext ctx)
    {
        this._ctx = ctx;
    }
    [HttpGet]
    public async Task<IActionResult> getAll()
    {
        var data = await this._ctx.BodyData.ToArrayAsync();
        return Ok(data);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> getData(int id)
    {
        var data = await this._ctx.BodyData.Where(bodyData => bodyData.UserId == id).ToListAsync();
        return Ok(data);
    }
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> createBodyDataItem([FromBody] BodyData b)
    {
        await this._ctx.BodyData.AddAsync(b);
        var save = await this._ctx.SaveChangesAsync();
        return  Ok(save);
    }
}