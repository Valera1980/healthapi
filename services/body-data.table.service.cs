using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public interface IBodyDataTable
{
    Task<int> getCount(int userId);
   Task<List<BodyData>> getDataByUserId(int userId);
}
public class BodyDataTableService : IBodyDataTable
{
    private IBodyDataRepository _repo;

    public BodyDataTableService(IBodyDataRepository bodyRepo)
    {
        this._repo = bodyRepo;
    }

    public async Task<int> getCount(int userId)
    {
        return await this._repo.getCount(userId);
    }
    public async Task<List<BodyData>> getDataByUserId(int userId)
    {
        return await this._repo.getDataByUserId(userId);
    }
}