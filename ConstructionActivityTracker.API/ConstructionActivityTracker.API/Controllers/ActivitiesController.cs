using Application.Features.Activities.Commands.Create;
using Application.Features.Activities.Commands.Delete;
using Application.Features.Activities.Commands.Update;
using Application.Features.Activities.Queries.GetById;
using Application.Features.Activities.Queries.GetList;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdActivityResponse>> GetById([FromRoute] int id)
    {
        GetByIdActivityQuery query = new() { Id = id };

        GetByIdActivityResponse response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListActivityListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListActivityQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListActivityListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<CreatedActivityResponse>> Add([FromBody] CreateActivityCommand command)
    {
        CreatedActivityResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }
    [HttpPut]
    public async Task<ActionResult<UpdatedActivityResponse>> Update([FromBody] UpdateActivityCommand command)
    {
        UpdatedActivityResponse response = await Mediator.Send(command);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedActivityResponse>> Delete([FromRoute] int id)
    {
        DeleteActivityCommand command = new() { Id = id };

        DeletedActivityResponse response = await Mediator.Send(command);

        return Ok(response);
    }
}
