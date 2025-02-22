using Application.Features.Activities.Constants;
using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using Application.Pipelines.Logging;
using Application.Pipelines.Transaction;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.Activities.Constants.ActivitiesOperationClaims;

namespace Application.Features.Activities.Commands.Update;

public class UpdateActivityCommand : IRequest<UpdatedActivityResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required int ActivityTypeId { get; set; }
    public required string Description { get; set; }
    public required DateTime ActivityDate { get; set; }

    public string[] Roles => [Admin, Write, ActivitiesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetActivities"];

    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, UpdatedActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;

        public UpdateActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
        }

        public async Task<UpdatedActivityResponse> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            Activity? Activity = await _activityRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            Activity = _mapper.Map(request, Activity);

            await _activityRepository.UpdateAsync(Activity!);

            UpdatedActivityResponse response = _mapper.Map<UpdatedActivityResponse>(Activity);
            return response;
        }
    }
}
