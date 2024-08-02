using Business.Abstract;
using core.Abstract;
using core.Helpers;
using data_access.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace games_admin_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Configuration : ControllerBase
    {
        private readonly IConfigurationService _configuration;
       
        public Configuration(IConfigurationService configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddFunc(ConfigurationModel model)
        {
            IServiceOutput? output = await _configuration.AddConfiguration(model);
            return await ActionOutput.GenerateAsync(output);
        }


        [HttpGet]
        [Route("get-list")]
        public async Task<IActionResult> GetListFunc()
        {
            IServiceOutput? output = await _configuration.GetList();
            return await ActionOutput.GenerateAsync(output);
        }


        [HttpGet]
        [Route("get-list-building-type")]
        public async Task<IActionResult> GetListBuildingTypeFunc()
        {
            IServiceOutput? output = await _configuration.GetListBuildingType();
            return await ActionOutput.GenerateAsync(output);
        }

        [HttpPost]
        [Route("add-building-type")]
        public async Task<IActionResult> AddBuildingTypeFunc(BuildingType model)
        {
            IServiceOutput? output = await _configuration.AddBuildingType(model);
            return await ActionOutput.GenerateAsync(output);
        }
    }
}
