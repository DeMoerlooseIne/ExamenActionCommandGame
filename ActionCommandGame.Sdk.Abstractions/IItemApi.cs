using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;

namespace ActionCommandGame.Sdk.Abstractions
{
    public interface IItemApi
    {
        Task<ServiceResult<ItemResult>> GetAsync(int id);
        Task<ServiceResult<IList<ItemResult>>> FindAsync();
        Task<ServiceResult<ItemResult>> Create(ItemResult itemResult);
        Task<ServiceResult<ItemResult>> Update(int id, ItemResult itemResult);
        Task<ServiceResult<ItemResult>> DeleteAsync(int id);
    }
}
