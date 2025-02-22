using Application.Features.Activities.Commands.Create;
using Application.Features.Activities.Commands.Delete;
using Application.Features.Activities.Commands.Update;
using Application.Features.Activities.Queries.GetById;
using Application.Features.Activities.Queries.GetList;
using Application.Repositories.Common;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Activities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateActivityCommand, Activity>();
        CreateMap<Activity, CreatedActivityResponse>();

        CreateMap<UpdateActivityCommand, Activity>();
        CreateMap<Activity, UpdatedActivityResponse>();

        CreateMap<DeleteActivityCommand, Activity>();
        CreateMap<Activity, DeletedActivityResponse>();

        CreateMap<Activity, GetByIdActivityResponse>();

        CreateMap<Activity, GetListActivityListItemDto>();
        CreateMap<IPaginate<Activity>, GetListResponse<GetListActivityListItemDto>>();

        CreateMap<Activity, GetListActivityListItemDto>()
            .ForMember(dest => dest.FullName,
                       opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
    }
}