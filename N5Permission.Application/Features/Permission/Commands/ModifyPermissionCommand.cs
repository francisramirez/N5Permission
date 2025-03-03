using MediatR;
using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Interfaces.Services.Permission;
using N5Permission.Application.Result;

namespace N5Permission.Application.Features.Permission.Commands
{
    public sealed record ModifyPermissionCommand(ModifyRequestPermission modifyRequest) : IRequest<Response<PermissionDto>>;
    public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, Response<PermissionDto>>
    {
        private readonly IPermisionService _permisionService;
        public ModifyPermissionHandler(IPermisionService permisionService) => _permisionService = permisionService;
        public async Task<Response<PermissionDto>> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken) => await _permisionService.ModifyRequestPermission(request.modifyRequest);
    }

}
