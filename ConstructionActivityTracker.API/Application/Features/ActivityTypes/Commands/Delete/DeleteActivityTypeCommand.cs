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

namespace Application.Features.ActivityTypes.Commands.Delete;

public class DeleteActivityTypeCommand : IRequest<DeletedActivityTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ActivityTypesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetActivityTypes"];

    public class DeleteActivityTypeCommandHandler : IRequestHandler<DeleteActivityTypeCommand, DeletedActivityTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityTypeRepository _activityTypeRepository;

        public DeleteActivityTypeCommandHandler(IMapper mapper, IActivityTypeRepository activityTypeRepository)
        {
            _mapper = mapper;
            _activityTypeRepository = activityTypeRepository;
        }

        public async Task<DeletedActivityTypeResponse> Handle(DeleteActivityTypeCommand request, CancellationToken cancellationToken)
        {
            ActivityType? activityType = await _activityTypeRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            await _activityTypeRepository.DeleteAsync(activityType!);

            DeletedActivityTypeResponse response = _mapper.Map<DeletedActivityTypeResponse>(activityType);
            return response;
        }
    }
}
