using Application.Pipelines.Authorization;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.Activities.Constants.ActivitiesOperationClaims;

namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityQuery : IRequest<GetByIdActivityResponse>
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdActivityQueryHandler : IRequestHandler<GetByIdActivityQuery, GetByIdActivityResponse>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetByIdActivityQueryHandler(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdActivityResponse> Handle(GetByIdActivityQuery request, CancellationToken cancellationToken)
        {
            Activity? Activity = await _activityRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdActivityResponse response = _mapper.Map<GetByIdActivityResponse>(Activity);
            return response;
        }
    }
}
