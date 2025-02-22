using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Activities.Commands.Delete;

public class DeletedActivityResponse : IResponse
{
    public int Id { get; set; }
}
