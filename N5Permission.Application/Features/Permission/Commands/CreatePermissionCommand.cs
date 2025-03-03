using MediatR;
using N5Permission.Application.Dtos.Permission;
using N5Permission.Application.Interfaces.Services.Permission;
using N5Permission.Application.Result;

namespace N5Permission.Application.Features.Permission.Commands
{
    public record CreateRequestPermissionCommand(CreateRequestPermission requestPermission) : IRequest<Response<PermissionDto>>;
    public class CreateRequestPermissionHandler : IRequestHandler<CreateRequestPermissionCommand, Response<PermissionDto>>
    {
        private readonly IPermisionService permisionService;
        public CreateRequestPermissionHandler(IPermisionService permisionService) => this.permisionService = permisionService;
        public async Task<Response<PermissionDto>> Handle(CreateRequestPermissionCommand request, CancellationToken cancellationToken) => await this.permisionService.CreateRequestPermission(request.requestPermission);
    }
}
