using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Activity : Entity<int>
{
    public int UserId { get; set; }
    public int ActivityTypeId { get; set; }
    public DateTime ActivityDate { get; set; }
    public string? Description { get; set; }

    public ActivityType? ActivityType { get; set; }
    public User? User { get; set; }
}
