using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActionCommandGame.Services.Abstractions;

public interface INegativeGameEventService
{
    Task<ServiceResult<NegativeGameEventResult>> GetRandomNegativeGameEvent(string authenticatedUserId);
    Task<ServiceResult<NegativeGameEventResult>> GetAsync(int id);
    Task<ServiceResult<IList<NegativeGameEventResult>>> FindAsync();
    Task<ServiceResult<NegativeGameEventResult>> Create(NegativeGameEventResult negativeGameEventResult);
    Task<ServiceResult<NegativeGameEventResult>> Update(int id, NegativeGameEventResult negativeGameEventResult);
    Task<ServiceResult> DeleteAsync(int id);
}