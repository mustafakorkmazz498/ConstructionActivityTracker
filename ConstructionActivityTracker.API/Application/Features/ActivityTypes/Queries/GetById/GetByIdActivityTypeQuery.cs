using Application.Pipelines.Authorization;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.ActivityTypes.Constants.ActivityTypesOperationClaims;

namespace Application.Features.ActivityTypes.Queries.GetById;

public class GetByIdActivityTypeQuery : IRequest<GetByIdActivityTypeResponse>
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdActivityTypeQueryHandler : IRequestHandler<GetByIdActivityTypeQuery, GetByIdActivityTypeResponse>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;

        public GetByIdActivityTypeQueryHandler(IActivityTypeRepository activityTypeRepository, IMapper mapper)
        {
            _activityTypeRepository = activityTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdActivityTypeResponse> Handle(GetByIdActivityTypeQuery request, CancellationToken cancellationToken)
        {
            ActivityType? activityType = await _activityTypeRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            GetByIdActivityTypeResponse response = _mapper.Map<GetByIdActivityTypeResponse>(activityType);
            return response;
        }
    }
}
