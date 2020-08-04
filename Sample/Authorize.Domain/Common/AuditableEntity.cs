using System;

namespace Authorize.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
        public bool IsEnabled { get; set; }

        protected AuditableEntity()
        {
            IsEnabled = true;
        }
    }
}
