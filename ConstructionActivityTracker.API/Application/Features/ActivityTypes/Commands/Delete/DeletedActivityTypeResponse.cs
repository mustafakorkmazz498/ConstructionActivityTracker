using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ActivityTypes.Commands.Delete;

public class DeletedActivityTypeResponse : IResponse
{
    public int Id { get; set; }
}