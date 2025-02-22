using Application.Features.ActivityTypes.Commands.Create;
using Application.Features.ActivityTypes.Commands.Delete;
using Application.Features.ActivityTypes.Commands.Update;
using Application.Features.ActivityTypes.Queries.GetById;
using Application.Features.ActivityTypes.Queries.GetList;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionActivityTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityTypesController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdActivityTypeResponse>> GetById([FromRoute] int id)
    {
        GetByIdActivityTypeQuery query = new() { Id = id };

        GetByIdActivityTypeResponse response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListActivityTypeListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListActivityTypesQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListActivityTypeListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<CreatedActivityTypeResponse>> Add([FromBody] CreateActivityTypeCommand command)
    {
        CreatedActivityTypeResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }
    [HttpPut]
    public async Task<ActionResult<UpdatedActivityTypeResponse>> Update([FromBody] UpdateActivityTypeCommand command)
    {
        UpdatedActivityTypeResponse response = await Mediator.Send(command);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedActivityTypeResponse>> Delete([FromRoute] int id)
    {
        DeleteActivityTypeCommand command = new() { Id = id };

        DeletedActivityTypeResponse response = await Mediator.Send(command);

        return Ok(response);
    }
}
