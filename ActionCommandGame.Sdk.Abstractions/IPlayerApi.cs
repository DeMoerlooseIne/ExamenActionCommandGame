using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk.Abstractions;

public interface IPlayerApi
{
    Task<ServiceResult<PlayerResult>> GetAsync(int id);
    Task<ServiceResult<IList<PlayerResult>>> Find(PlayerFilter filter);
    Task<ServiceResult<PlayerResult>> Create(PlayerResult playerResult);
    Task<ServiceResult<PlayerResult>> Update(int id, PlayerResult playerResult);
    Task<ServiceResult<PlayerResult>> DeleteAsync(int id);
}