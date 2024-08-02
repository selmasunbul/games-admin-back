using business.Abstract;
using core.Abstract;
using core.Helpers;
using data_access.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace games_admin_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountsController(IUserService userService,
                                  IConfiguration configuration,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            IServiceOutput? output = await _userService.Register(model);
            return await ActionOutput.GenerateAsync(output);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            IServiceOutput? output = await _userService.Login(model);
            return await ActionOutput.GenerateAsync(output);
        }
    }
}
