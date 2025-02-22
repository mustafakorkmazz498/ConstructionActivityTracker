using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using Application.Repositories;
using Application.Repositories.Common;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.ActivityTypes.Constants.ActivityTypesOperationClaims;

namespace Application.Features.ActivityTypes.Queries.GetList;

public class GetListActivityTypesQuery : IRequest<GetListResponse<GetListActivityTypeListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListActivityTypes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetActivityTypes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListActivityTypesQueryHandler : IRequestHandler<GetListActivityTypesQuery, GetListResponse<GetListActivityTypeListItemDto>>
    {
        private readonly IActivityTypeRepository _activityTypesRepository;
        private readonly IMapper _mapper;

        public GetListActivityTypesQueryHandler(IActivityTypeRepository activityTypeRepository, IMapper mapper)
        {
            _activityTypesRepository = activityTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListActivityTypeListItemDto>> Handle(GetListActivityTypesQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ActivityType> activityTypes = await _activityTypesRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListActivityTypeListItemDto> response = _mapper.Map<GetListResponse<GetListActivityTypeListItemDto>>(activityTypes);
            return response;
        }
    }
}
