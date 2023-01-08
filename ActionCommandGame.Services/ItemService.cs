using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActionCommandGame.Services;

public class ItemService : IItemService
{
    private readonly ActionCommandGameDbContext _dbContext;

    public ItemService(ActionCommandGameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<ItemResult>> GetAsync(int id, string authenticatedUserId)
    {
        var item = await _dbContext.Items
            .ProjectToResult()
            .SingleOrDefaultAsync(i => i.Id == id);

        return new ServiceResult<ItemResult>(item);
    }

    public async Task<ServiceResult<IList<ItemResult>>> FindAsync(string authenticatedUserId)
    {
        var items = await _dbContext.Items
            .ProjectToResult()
            .ToListAsync();

        return new ServiceResult<IList<ItemResult>>(items);
    }


    public async Task<ServiceResult<ItemResult>> Create(ItemResult itemResult)
    {
        var item = new Item
        {
            Id = itemResult.Id,
            Name = itemResult.Name,
            ActionCooldownSeconds = itemResult.ActionCooldownSeconds,
            Attack = itemResult.Attack,
            Defense = itemResult.Defense,
            Fuel = itemResult.Fuel,
            Description = itemResult.Description,
            ImageUrl = itemResult.ImageUrl,
            Price = itemResult.Price
        };

        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();

        return await GetAsync(item.Id, null);
    }


    public async Task<ServiceResult<ItemResult>> Update(int id, ItemResult itemResult)
    {
        var dbItem = await _dbContext.Items
            .SingleOrDefaultAsync(i => i.Id == id);

        if (dbItem == null) return null;

        dbItem.Name = itemResult.Name;
        dbItem.ActionCooldownSeconds = itemResult.ActionCooldownSeconds;
        dbItem.Attack = itemResult.Attack;
        dbItem.Defense = itemResult.Defense;
        dbItem.Fuel = itemResult.Fuel;
        dbItem.Description = itemResult.Description;
        dbItem.ImageUrl = itemResult.ImageUrl;
        dbItem.Price = itemResult.Price;

        await _dbContext.SaveChangesAsync();

        return await GetAsync(dbItem.Id, null);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var item = await _dbContext.Items
            .SingleOrDefaultAsync(p => p.Id == id);

        if (item == null) return new ServiceResult().NotFound();

        _dbContext.Items.Remove(item);
        await _dbContext.SaveChangesAsync();

        return new ServiceResult();
    }
}