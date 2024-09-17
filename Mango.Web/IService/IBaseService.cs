using Mango.Web.Models;

namespace Mango.Web.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);

        Task Test();
    }
}
