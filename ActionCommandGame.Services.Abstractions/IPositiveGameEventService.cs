using System.Collections.Generic;
using System.Threading.Tasks;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IPositiveGameEventService
    {
        Task<ServiceResult<PositiveGameEventResult>> GetRandomPositiveGameEvent(bool hasAttackItem, string authenticatedUserId);
        Task<ServiceResult<PositiveGameEventResult>> GetAsync(int id);
        Task<ServiceResult<IList<PositiveGameEventResult>>> FindAsync();
        Task<ServiceResult<PositiveGameEventResult>> Create(PositiveGameEventResult positiveGameEventResult);
        Task<ServiceResult<PositiveGameEventResult>> Update(int id, PositiveGameEventResult positiveGameEventResult);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
