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

namespace Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommand : IRequest<DeletedActivityResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ActivitiesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetActivities"];

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, DeletedActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;

        public DeleteActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
        }

        public async Task<DeletedActivityResponse> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            Activity? activity = await _activityRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            await _activityRepository.DeleteAsync(activity!);

            DeletedActivityResponse response = _mapper.Map<DeletedActivityResponse>(activity);
            return response;
        }
    }
}
