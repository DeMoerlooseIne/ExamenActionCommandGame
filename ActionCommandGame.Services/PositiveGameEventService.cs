using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Helpers;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionCommandGame.Services
{
    public class PositiveGameEventService : IPositiveGameEventService
    {
        private readonly ActionCommandGameDbContext _database;

        public PositiveGameEventService(ActionCommandGameDbContext database)
        {
            _database = database;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> GetRandomPositiveGameEvent(bool hasAttackItem, string authenticatedUserId)
        {
            var query = _database.PositiveGameEvents.AsQueryable();

            //If we don't have an attack item, we can only get low-reward items.
            if (!hasAttackItem)
            {
                query = query.Where(p => p.Money < 50);
            }

            var gameEvents = await query
                .ProjectToResult()
                .ToListAsync();

            var randomEvent = GameEventHelper.GetRandomPositiveGameEvent(gameEvents);

            return new ServiceResult<PositiveGameEventResult>(randomEvent);
        }


        public async Task<ServiceResult<PositiveGameEventResult>> GetAsync(int id)
        {
            var positiveGameEvent = await _database.PositiveGameEvents
                .ProjectToResult()
                .SingleOrDefaultAsync(p => p.Id == id);

            return new ServiceResult<PositiveGameEventResult>(positiveGameEvent);
        }

        public async Task<ServiceResult<IList<PositiveGameEventResult>>> FindAsync()
        {
            var positiveGameEvents = await _database.PositiveGameEvents
                .ProjectToResult()
                .ToListAsync();

            return new ServiceResult<IList<PositiveGameEventResult>>(positiveGameEvents);
        }

        public async Task<ServiceResult<PositiveGameEventResult>> Create(PositiveGameEventResult positiveGameEventResult)
        {
            var positiveGameEvent = new PositiveGameEvent()
            {
                Id = positiveGameEventResult.Id,
                Name = positiveGameEventResult.Name,
                Description = positiveGameEventResult.Description,
                Experience = positiveGameEventResult.Experience,
                Money = positiveGameEventResult.Money,
                Probability = positiveGameEventResult.Probability,
            };

            _database.PositiveGameEvents.Add(positiveGameEvent);
            await _database.SaveChangesAsync();

            return await GetAsync(positiveGameEvent.Id);
        }

        public async Task<ServiceResult<PositiveGameEventResult>> Update(int id, PositiveGameEventResult positiveGameEventResult)
        {
            var dbpositiveGameEvent = await _database.PositiveGameEvents
                .SingleOrDefaultAsync(p => p.Id == id);

            if (dbpositiveGameEvent == null)
            {
                return null;
            }

            dbpositiveGameEvent.Name = positiveGameEventResult.Name;
            dbpositiveGameEvent.Money = positiveGameEventResult.Money;
            dbpositiveGameEvent.Experience = positiveGameEventResult.Experience;
            dbpositiveGameEvent.Description = positiveGameEventResult.Description;
            dbpositiveGameEvent.Probability = positiveGameEventResult.Probability;

            await _database.SaveChangesAsync();

            return await GetAsync(dbpositiveGameEvent.Id);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var positiveGameEvent = await _database.PositiveGameEvents
                .SingleOrDefaultAsync(p => p.Id == id);

            if (positiveGameEvent == null)
            {
                return new ServiceResult().NotFound();
            }

            _database.PositiveGameEvents.Remove(positiveGameEvent);
            await _database.SaveChangesAsync();

            return new ServiceResult();
        }
    }
}