using core.Abstract;
using data_access.Models;

namespace business.Abstract
{
    public interface IUserService
    {
        Task<IServiceOutput> Register(RegisterModel model);
        Task<IServiceOutput> Login(LoginModel model);
    
    }
}
