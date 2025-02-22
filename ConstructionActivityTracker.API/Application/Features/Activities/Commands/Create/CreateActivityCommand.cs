using Application.Features.Activities.Constants;
using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using Application.Pipelines.Logging;
using Application.Pipelines.Transaction;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static Application.Features.Activities.Constants.ActivitiesOperationClaims;

namespace Application.Features.Activities.Commands.Create;

public class CreateActivityCommand : IRequest<CreatedActivityResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required int ActivityTypeId { get; set; }
    public required string Description { get; set; }
    public required DateTime ActivityDate { get; set; }

    public string[] Roles => [Admin, Write, ActivitiesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetActivities"];

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, CreatedActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreatedActivityResponse> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            Activity activity = _mapper.Map<Activity>(request);

            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                activity.UserId = int.Parse(userIdClaim.Value);
            }
            else
            {
                throw new UnauthorizedAccessException("Kullanıcı bilgisi alınamadı.");
            }

            await _activityRepository.AddAsync(activity);

            CreatedActivityResponse response = _mapper.Map<CreatedActivityResponse>(activity);
            return response;
        }
    }

}