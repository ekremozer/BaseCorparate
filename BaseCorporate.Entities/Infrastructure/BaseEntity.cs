using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseCorporate.Entities.Entities;

namespace BaseCorporate.Entities.Infrastructure
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedByUserId { get; set; }
        public User UpdatedByUser { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedByUserId { get; set; }
        public User DeletedByUser { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }

        public BaseEntity()
        {
            Uid = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            UpdatedByUserId = null;
            DeletedAt = null;
            DeletedByUserId = null;
            IsActive = true;
            Deleted = false;
        }
    }
}
