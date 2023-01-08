using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Helpers;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActionCommandGame.Services;

public class NegativeGameEventService : INegativeGameEventService
{
    private readonly ActionCommandGameDbContext _database;

    public NegativeGameEventService(ActionCommandGameDbContext database)
    {
        _database = database;
    }

    public async Task<ServiceResult<NegativeGameEventResult>> GetRandomNegativeGameEvent(string authenticatedUserId)
    {
        var gameEvents = await Find(authenticatedUserId);
        var randomEvent = GameEventHelper.GetRandomNegativeGameEvent(gameEvents.Data);
        return new ServiceResult<NegativeGameEventResult>(randomEvent);
    }

    public async Task<ServiceResult<NegativeGameEventResult>> GetAsync(int id)
    {
        var negativeGameEvent = await _database.NegativeGameEvents
            .ProjectToResult()
            .SingleOrDefaultAsync(p => p.Id == id);

        return new ServiceResult<NegativeGameEventResult>(negativeGameEvent);
    }

    public async Task<ServiceResult<IList<NegativeGameEventResult>>> FindAsync()
    {
        var negativeGameEvents = await _database.NegativeGameEvents
            .ProjectToResult()
            .ToListAsync();

        return new ServiceResult<IList<NegativeGameEventResult>>(negativeGameEvents);
    }

    public async Task<ServiceResult<NegativeGameEventResult>> Create(NegativeGameEventResult negativeGameEventResult)
    {
        var negativeGameEvent = new NegativeGameEvent
        {
            Id = negativeGameEventResult.Id,
            Name = negativeGameEventResult.Name,
            Description = negativeGameEventResult.Description,
            DefenseWithArmorDescription = negativeGameEventResult.DefenseWithArmorDescription,
            DefenseWithoutArmorDescription = negativeGameEventResult.DefenseWithoutArmorDescription,
            DefenseLoss = negativeGameEventResult.DefenseLoss,
            Probability = negativeGameEventResult.Probability
        };

        _database.NegativeGameEvents.Add(negativeGameEvent);
        await _database.SaveChangesAsync();

        return await GetAsync(negativeGameEventResult.Id);
    }

    public async Task<ServiceResult<NegativeGameEventResult>> Update(int id,
        NegativeGameEventResult negativeGameEventResult)
    {
        var dbNegativeGameEvent = await _database.NegativeGameEvents
            .SingleOrDefaultAsync(p => p.Id == id);

        if (dbNegativeGameEvent == null) return null;

        dbNegativeGameEvent.Name = negativeGameEventResult.Name;
        dbNegativeGameEvent.Description = negativeGameEventResult.Description;
        dbNegativeGameEvent.DefenseWithArmorDescription = negativeGameEventResult.DefenseWithArmorDescription;
        dbNegativeGameEvent.DefenseWithoutArmorDescription = negativeGameEventResult.DefenseWithoutArmorDescription;
        dbNegativeGameEvent.DefenseLoss = negativeGameEventResult.DefenseLoss;
        dbNegativeGameEvent.Probability = negativeGameEventResult.Probability;

        await _database.SaveChangesAsync();

        return await GetAsync(dbNegativeGameEvent.Id);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var negativeGameEvent = await _database.NegativeGameEvents
            .SingleOrDefaultAsync(p => p.Id == id);

        if (negativeGameEvent == null) return new ServiceResult().NotFound();

        _database.NegativeGameEvents.Remove(negativeGameEvent);
        await _database.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult<IList<NegativeGameEventResult>>> Find(string authenticatedUserId)
    {
        var negativeGameEvents = await _database.NegativeGameEvents
            .ProjectToResult()
            .ToListAsync();

        return new ServiceResult<IList<NegativeGameEventResult>>(negativeGameEvents);
    }
}