
using N5Permission.Application.Result;

namespace N5Permission.Application.Interfaces.Services.Base
{
    public interface IBaseService<TDtoSave, TDtoUpdate, TDtoRemove, TResult>
    {
        Task<Response<TResult>> GetAllAsync();
        Task<Response<TResult>> GetByIdAsync(int Id);
        Task<Response<TResult>> UpdateAsync(TDtoUpdate dto);
        Task<Response<TResult>> RemoveAsync(TDtoRemove dto);
        Task<Response<TResult>> SaveAsync(TDtoSave dto);
    }
}
