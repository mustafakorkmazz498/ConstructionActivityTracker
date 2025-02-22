using Application.Features.ActivityTypes.Constants;
using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using Application.Pipelines.Logging;
using Application.Pipelines.Transaction;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.ActivityTypes.Constants.ActivityTypesOperationClaims;

namespace Application.Features.ActivityTypes.Commands.Update;

public class UpdateActivityTypeCommand : IRequest<UpdatedActivityTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }

    public string[] Roles => [Admin, Write, ActivityTypesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetActivityTypes"];

    public class UpdateActivityTypeCommandHandler : IRequestHandler<UpdateActivityTypeCommand, UpdatedActivityTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityTypeRepository _activityTypeRepository;

        public UpdateActivityTypeCommandHandler(IMapper mapper, IActivityTypeRepository activityTypeRepository)
        {
            _mapper = mapper;
            _activityTypeRepository = activityTypeRepository;
        }

        public async Task<UpdatedActivityTypeResponse> Handle(UpdateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            ActivityType? activityType = await _activityTypeRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            activityType = _mapper.Map(request, activityType);

            await _activityTypeRepository.UpdateAsync(activityType!);

            UpdatedActivityTypeResponse response = _mapper.Map<UpdatedActivityTypeResponse>(activityType);
            return response;
        }
    }
}