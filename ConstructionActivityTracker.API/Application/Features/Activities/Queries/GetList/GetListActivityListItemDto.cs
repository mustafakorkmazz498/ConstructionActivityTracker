using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Queries.GetList;

public class GetListActivityListItemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public int ActivityTypeId { get; set; }
    public string? ActivityTypeName { get; set; }
    public string? ActivityTypeCode { get; set; }
    public DateTime? ActivityDate { get; set; }
    public string? Description { get; set; }
}

