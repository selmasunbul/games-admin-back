using core.Abstract;
using data_access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IConfigurationService
    {
        Task<IServiceOutput> AddConfiguration(ConfigurationModel model);
        Task<IServiceOutput> GetList();
        Task<IServiceOutput> GetListBuildingType();
        Task<IServiceOutput> AddBuildingType(BuildingType model);
            
   }
}
