using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class ActivityTypeRepository : EfRepositoryBase<ActivityType, int, BaseDbContext>, IActivityTypeRepository
{
    public ActivityTypeRepository(BaseDbContext context) : base(context)
    {
    }
}
