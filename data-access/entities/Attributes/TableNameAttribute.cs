using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.entities.Attributes
{
    public class TableNameAttribute : Attribute
    {
        public string? Name { get; set; }
    }
}
