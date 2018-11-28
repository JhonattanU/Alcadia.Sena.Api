using Dapper.Contrib.Extensions;
using System;

namespace Alcadia.Sena.Models
{
    public abstract class Audit
    {
        private Guid _id;
        [ExplicitKey]
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = default(Guid) != value ? value : default(Guid) != _id ? _id : Guid.NewGuid();
            }
        }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
        [Computed]
        public byte[] RowVersion { get; set; }

        public Audit()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
