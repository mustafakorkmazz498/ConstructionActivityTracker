using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Commands.Create;

public class CreatedActivityResponse : IResponse
{
    public int Id { get; set; }
    public int? ActivityTypeId { get; set; }
    public string? Description { get; set; }
    public DateTime? ActivityDate { get; set; }
}
