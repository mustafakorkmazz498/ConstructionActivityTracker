using Application.Dtos;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public int OperationClaimId { get; set; }
    public string? OperationClaimName { get; set; }
}