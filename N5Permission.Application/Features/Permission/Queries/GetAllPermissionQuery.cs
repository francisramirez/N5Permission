using MediatR;
using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Interfaces.Services.Permission;
using N5Permission.Application.Result;

namespace N5Permission.Application.Features.Permission.Queries
{
    public sealed record GetPermissionsQuery : IRequest<Response<List<PermissionDto>>>
    {
        public string PermissionId { get; set; }
    }
    public class GetPermissionsHandler(IPermisionService permisionService) : IRequestHandler<GetPermissionsQuery, Response<List<PermissionDto>>>
    {
        private readonly IPermisionService _permisionService = permisionService;
        public async Task<Response<List<PermissionDto>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken) => await _permisionService.GetPermissions(request.PermissionId);
    }

}
