using Application.Features.ActivityTypes.Commands.Create;
using Application.Features.ActivityTypes.Commands.Delete;
using Application.Features.ActivityTypes.Commands.Update;
using Application.Features.ActivityTypes.Queries.GetById;
using Application.Features.ActivityTypes.Queries.GetList;
using Application.Repositories.Common;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ActivityTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateActivityTypeCommand, ActivityType>();
        CreateMap<ActivityType, CreatedActivityTypeResponse>();

        CreateMap<UpdateActivityTypeCommand, ActivityType>();
        CreateMap<ActivityType, UpdatedActivityTypeResponse>();

        CreateMap<DeleteActivityTypeCommand, ActivityType>();
        CreateMap<ActivityType, DeletedActivityTypeResponse>();

        CreateMap<ActivityType, GetByIdActivityTypeResponse>();
        CreateMap<ActivityType, GetListActivityTypeListItemDto>();
        CreateMap<IPaginate<ActivityType>, GetListResponse<GetListActivityTypeListItemDto>>();
    }
}
