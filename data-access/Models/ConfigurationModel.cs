using data_access.entities.MongoDBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Models
{


    public class JoinedResult
    {
        public ConfigurationEntity Configuration { get; set; }
        public BuildingTypeEntity BuildingType { get; set; }
    }


    public class ConfigurationModel
    {
        public string OId { get; set; }

        public string BuildingType { get; set; }

        public int BuildingCost { get; set; }

        public int ConstructionTime { get; set; }
    }

    public class BuildingType
    {
        public string OId { get; set; }
        public string Name { get; set; }

    }
}
