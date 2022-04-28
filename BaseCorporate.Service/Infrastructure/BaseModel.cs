using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Model;

namespace BaseCorporate.Service.Infrastructure
{
    public class BaseModel
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public UserModel SessionUser { get; set; }
        public int SessionUserId { get; set; }

        public BaseModel()
        {
            IsActive = true;
        }
    }
}
