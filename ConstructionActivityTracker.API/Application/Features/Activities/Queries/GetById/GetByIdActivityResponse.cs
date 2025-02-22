using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityResponse : IResponse
{
    public int Id { get; set; }
    public int ActivityTypeId { get; set; }
    public string? Description { get; set; }
    public DateTime? ActivityDate { get; set; }
}

