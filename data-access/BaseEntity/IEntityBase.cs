using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.BaseEntity
{
    public interface IEntityBase
    {
        public int Id { get; set; }

        /// <summary>
        /// Kaydın oluşturulma tarihini alır veya ayarlar.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Kaydın son düzenlenme tarihini alır veya ayarlar.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Kaydın silinip silinmediğini alır veya ayarlar.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
