using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk.Abstractions
{
    public interface INegativeGameEventApi
    {
        Task<ServiceResult<NegativeGameEventResult>> GetAsync(int id);
        Task<ServiceResult<IList<NegativeGameEventResult>>> Find();
        Task<ServiceResult<NegativeGameEventResult>> Create(NegativeGameEventResult negativeGameEventResult);
        Task<ServiceResult<NegativeGameEventResult>> Update(int id, NegativeGameEventResult negativeGameEventResult);
        Task<ServiceResult<NegativeGameEventResult>> DeleteAsync(int id);
    }
}
