using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ActivityTypes.Commands.Update;

public class UpdatedActivityTypeResponse : IResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}
