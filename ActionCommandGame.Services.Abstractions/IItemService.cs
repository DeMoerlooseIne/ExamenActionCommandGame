using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActionCommandGame.Services.Abstractions
{
    public interface IItemService
    {
        Task<ServiceResult<ItemResult>> GetAsync(int id, string authenticatedUserId);
        Task<ServiceResult<IList<ItemResult>>> FindAsync(string authenticatedUserId);
        Task<ServiceResult<ItemResult>> Create(ItemResult itemResult);
        Task<ServiceResult<ItemResult>> Update(int id, ItemResult itemResult);
        Task<ServiceResult> DeleteAsync(int id);

    }
}
