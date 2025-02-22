using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using Application.Repositories;
using Application.Repositories.Common;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Activities.Constants.ActivitiesOperationClaims;

namespace Application.Features.Activities.Queries.GetList;

public class GetListActivityQuery : IRequest<GetListResponse<GetListActivityListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListActivities({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetActivities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListActivityQueryHandler : IRequestHandler<GetListActivityQuery, GetListResponse<GetListActivityListItemDto>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetListActivityQueryHandler(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListActivityListItemDto>> Handle(GetListActivityQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Activity> activities = await _activityRepository.GetListAsync(
                include: query => query.Include(a => a.ActivityType)
                        .Include(a => a.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListActivityListItemDto> response = _mapper.Map<GetListResponse<GetListActivityListItemDto>>(activities);
            return response;
        }

    }
}
