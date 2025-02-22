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
namespace Application.Features.ActivityTypes.Commands.Create;

public class CreateActivityTypeCommand : IRequest<CreatedActivityTypeResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Code { get; set; }

    public string[] Roles => [Admin, Write, ActivityTypesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetActivityTypes"];

    public class CreateActivityTypeCommandHandler : IRequestHandler<CreateActivityTypeCommand, CreatedActivityTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityTypeRepository _activityTypeRepository;

        public CreateActivityTypeCommandHandler(IMapper mapper, IActivityTypeRepository activityTypeRepository)
        {
            _mapper = mapper;
            _activityTypeRepository = activityTypeRepository;
        }

        public async Task<CreatedActivityTypeResponse> Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            ActivityType activityType = _mapper.Map<ActivityType>(request);

            await _activityTypeRepository.AddAsync(activityType);

            CreatedActivityTypeResponse response = _mapper.Map<CreatedActivityTypeResponse>(activityType);
            return response;
        }
    }
}
