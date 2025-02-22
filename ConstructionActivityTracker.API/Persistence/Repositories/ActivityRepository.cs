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

internal class ActivityRepository : EfRepositoryBase<Activity, int, BaseDbContext>, IActivityRepository
{
    public ActivityRepository(BaseDbContext context) : base(context)
    {
    }
}