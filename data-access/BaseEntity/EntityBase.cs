using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace data_access.BaseEntity
{
    [DataContract]
    public class EntityBase : IEntityBase
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        [Column]
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

        [Column]
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}
