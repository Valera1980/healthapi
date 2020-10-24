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
    private IBodyDataTable _dataTableService;

    public BodyDataController(AppMainContext ctx, IBodyDataTable dataTableService)
    {
        this._ctx = ctx;
        this._dataTableService = dataTableService;
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
        return Ok(save);
    }
    [HttpPost]
    [Route("table")]
    public async Task<IActionResult> queryBodyDataTable([FromBody] ParamsBodyDataTable p)
    {
        int count = await this._dataTableService.getCount(p.UserId);
        List<BodyData> data = await this._dataTableService.getPaginationData(p.CurrentPage, p.PageSize, p.UserId);
        return Ok(
            new WrapperPagination<List<BodyData>>
            {
                pageSize = p.PageSize,
                currentPage = p.CurrentPage,
                totalCount = count,
                data = data
            });
    }
}