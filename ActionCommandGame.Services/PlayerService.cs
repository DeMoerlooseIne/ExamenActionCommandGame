using ActionCommandGame.Model;
using ActionCommandGame.Repository;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Extensions;
using ActionCommandGame.Services.Extensions.Filters;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActionCommandGame.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ActionCommandGameDbContext _database;

        public PlayerService(ActionCommandGameDbContext database)
        {
            _database = database;
        }

        public async Task<ServiceResult<PlayerResult>> GetAsync(int id, string authenticatedUserId)
        {
            var player = await _database.Players
                .ProjectToResult()
                .SingleOrDefaultAsync(p => p.Id == id);

            return new ServiceResult<PlayerResult>(player);
        }

        public async Task<ServiceResult<IList<PlayerResult>>> FindAsync(PlayerFilter filter, string authenticatedUserId)
        {
            var players = await _database.Players
                .ApplyFilter(filter, authenticatedUserId)
                .ProjectToResult()
                .ToListAsync();

            return new ServiceResult<IList<PlayerResult>>(players);
        }

        public async Task<ServiceResult<PlayerResult>> Create(PlayerResult playerResult, string authenticatedUserId)
        {
            var player = new Player()
            {
                Id = playerResult.Id,
                Name = playerResult.Name,
                UserId = authenticatedUserId,
                Experience = playerResult.Experience,
                ImageName = playerResult.ImageName,
                Money = playerResult.Money,
                LastActionExecutedDateTime = playerResult.LastActionExecutedDateTime,
            };

            _database.Players.Add(player);
            await _database.SaveChangesAsync();

            return await GetAsync(player.Id, authenticatedUserId);
        }

        public async Task<ServiceResult<PlayerResult>> Update(int id, PlayerResult playerResult, string authenticatedUserId)
        {
            var dbPlayer = await _database.Players
                .SingleOrDefaultAsync(p => p.Id == id);

            if (dbPlayer == null)
            {
                return null;
            }

            dbPlayer.Name = playerResult.Name;
            dbPlayer.Money = playerResult.Money;
            dbPlayer.Experience = playerResult.Experience;
            dbPlayer.ImageName = playerResult.ImageName;

            await _database.SaveChangesAsync();

            return await GetAsync(dbPlayer.Id, authenticatedUserId);
        }

        public async Task<ServiceResult> DeleteAsync(int id, string authenticatedUserId)
        {
            var player = await _database.Players
                .SingleOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return new ServiceResult().NotFound();
            }

            _database.Players.Remove(player);
            await _database.SaveChangesAsync();

            return new ServiceResult();
        }
    }
}
