﻿using Application.Features.UserOperationClaims.Constants;
using Application.Pipelines.Authorization;
using Application.Repositories;
using Application.Repositories.Common;
using Application.Requests;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;


namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQuery : IRequest<GetListResponse<GetListUserOperationClaimListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin];

    public GetListUserOperationClaimQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserOperationClaimQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListUserOperationClaimQueryHandler
        : IRequestHandler<GetListUserOperationClaimQuery, GetListResponse<GetListUserOperationClaimListItemDto>>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;

        public GetListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserOperationClaimListItemDto>> Handle(
            GetListUserOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                include: query => query.Include(a => a.User)
                        .Include(a => a.OperationClaim),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false
            );

            GetListResponse<GetListUserOperationClaimListItemDto> mappedUserOperationClaimListModel = _mapper.Map<
                GetListResponse<GetListUserOperationClaimListItemDto>
            >(userOperationClaims);
            return mappedUserOperationClaimListModel;
        }
    }
}

