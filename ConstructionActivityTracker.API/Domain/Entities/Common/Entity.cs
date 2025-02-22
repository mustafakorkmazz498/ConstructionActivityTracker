using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common;

public abstract class Entity<TId> : IEntity<TId>, IEntityTimestamps
{
    public TId Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Entity()
    {
        Id = default(TId);
    }

    public Entity(TId id)
    {
        Id = id;
    }
}
