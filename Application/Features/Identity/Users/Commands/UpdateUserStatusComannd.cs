﻿using Application.Models.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Users.Commands
{
    public class UpdateUserStatusCommand : IRequest<IResponseWrapper>
    {
        public ChangeUserStatusRequest ChangeUserStatus { get; set; }
    }


    public class UpdateUserStatusCommandHandler(IUserService userService) 
        : IRequestHandler<UpdateUserStatusCommand, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;
        public async Task<IResponseWrapper> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService
                .ActivateOrDeactivateAsync(request.ChangeUserStatus.UserId, request.ChangeUserStatus.Activation);
            return await ResponseWrapper<string>
                .SuccessAsync(data: userId, message: request.ChangeUserStatus.Activation ?
                    "User activated successfully" : "User de-activated successfully");
        }
    }
}
