using Business.Abstract;
using Business.Base;
using core.Abstract;
using core.Helpers;
using data_access.entities.MongoDBEntities;
using data_access.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IMongoServiceBase<ConfigurationEntity> _configuration;
        private readonly IMongoServiceBase<BuildingTypeEntity> _buildingType;

        public ConfigurationService(IMongoServiceBase<ConfigurationEntity> configuration, IMongoServiceBase<BuildingTypeEntity> buildingType)
        {
            _configuration = configuration;
            _buildingType = buildingType;
        }


        public async Task<IServiceOutput> AddConfiguration(ConfigurationModel model)
        {
            try
            {
                var newConfigurationEntity = new ConfigurationEntity
                {
                    BuildingType = model.BuildingType,
                    BuildingCost = model.BuildingCost,
                    ConstructionTime = model.ConstructionTime,
                };

                // Veriyi MongoDB'ye ekle
                await _configuration.InsertAsync(newConfigurationEntity);

                // Başarılı yanıt döndür
                return await ServiceOutput.GenerateAsync(200, true);
            }
            catch (Exception ex)
            {
                // Hata yanıtı döndür
                return await ServiceOutput.GenerateAsync(500, false, "Sunucu hatası: " + ex.Message, data: null);
            }

        }




        public async Task<IServiceOutput> GetList()
        {
            try
            {

                var configurations = _configuration.GetAllList();
                var buildingTypes = _buildingType.GetAllList();

                // Join işlemi
                var joinedData = from config in configurations
                                 join type in buildingTypes
                                 on config.BuildingType equals type.OId
                                 select new JoinedResult
                                 {
                                     Configuration = config,
                                     BuildingType = type
                                 };




                // Başarılı yanıt döndür
                return await ServiceOutput.GenerateAsync(200, true, data: joinedData.ToList());
            }
            catch (Exception ex)
            {
                // Hata yanıtı döndür
                return await ServiceOutput.GenerateAsync(500, false, "Sunucu hatası: " + ex.Message, data: null);
            }
        }


        public async Task<IServiceOutput> GetListBuildingType()
        {
            try
            {
                // MongoDB'den tüm belgeleri çek
                var configurations = _buildingType.GetAllList();

                // Başarılı yanıt döndür
                return await ServiceOutput.GenerateAsync(200, true, data: configurations);
            }
            catch (Exception ex)
            {
                // Hata yanıtı döndür
                return await ServiceOutput.GenerateAsync(500, false, "Sunucu hatası: " + ex.Message, data: null);
            }
        }


        public async Task<IServiceOutput> AddBuildingType(BuildingType model)
        {
            try
            {
                var newEntity = new BuildingTypeEntity
                {
                   Name= model.Name
                };

                // Veriyi MongoDB'ye ekle
                await _buildingType.InsertAsync(newEntity);

                // Başarılı yanıt döndür
                return await ServiceOutput.GenerateAsync(200, true);
            }
            catch (Exception ex)
            {
                // Hata yanıtı döndür
                return await ServiceOutput.GenerateAsync(500, false, "Sunucu hatası: " + ex.Message, data: null);
            }

        }

    }
}
