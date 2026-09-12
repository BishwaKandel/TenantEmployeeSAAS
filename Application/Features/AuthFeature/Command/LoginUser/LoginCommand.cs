using Domain.DTO.LoginDataTransferObjects;
using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.AuthFeature.Command.LoginUser
{
    public class LoginCommand : LoginRequestDto, IRequest<ResponseModel<TokenResponseDto>>
    {
    }
}
