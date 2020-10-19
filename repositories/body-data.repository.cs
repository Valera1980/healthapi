using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IBodyDataRepository
{
    Task<int> getCount(int userId);
    Task<List<BodyData>> getDataByUserId(int userId);

}
public class BodyDataRepository : IBodyDataRepository
{
    private AppMainContext _ctx;

    public BodyDataRepository(AppMainContext ctx)
    {
        this._ctx = ctx;
    }
    public async Task<int> getCount(int userId)
    {
        return await this._ctx.BodyData.CountAsync(d => d.UserId == userId);
    }
    public async Task<List<BodyData>> getDataByUserId(int userId)
    {
        return await this._ctx.BodyData.Where(bodyData => bodyData.UserId == userId).ToListAsync();
    }

}