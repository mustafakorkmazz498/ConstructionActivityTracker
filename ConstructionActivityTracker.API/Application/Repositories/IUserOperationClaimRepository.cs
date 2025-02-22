using Application.Repositories.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim,int>
{
    Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId);
}
