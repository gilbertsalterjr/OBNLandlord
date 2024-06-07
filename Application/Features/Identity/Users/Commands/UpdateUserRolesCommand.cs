using Application.Models.Wrapper;
using MediatR;

namespace Application.Features.Identity.Users.Commands
{
    public class UpdateUserRolesCommand : IRequest<IResponseWrapper>
    {
        public string RoleId { get; set; }
        public UserRolesRequest UserRolesRequest { get; set; }
    }


    public class UpdateUserRolesCommandHanler(IUserService userService) : IRequestHandler<UpdateUserRolesCommand, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;
        public async Task<IResponseWrapper> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.AssignRolesAsync(request.RoleId, request.UserRolesRequest);
            return await ResponseWrapper<string>.SuccessAsync(data: userId, message: "User role updated successfully.");
        }
    }

}
