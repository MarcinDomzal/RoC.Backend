using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoC.Application.Exceptions;
using RoC.Application.Interfaces;
using RoC.Application.Logic.Abstractions;
using RoC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoC.Application.Logic.User
{
    public static class LogoutCommand
    {
        public class Request : IRequest<Result>
        {
        }

        public class Result
        {
        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
           
            public Handler(ICurrentAccountProvider currentaccountProvider, IApplicationDbContext applicationDbContext) : base(currentaccountProvider, applicationDbContext) 
            {
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Result()
                {
                    //gdyby była potrzebne info o wylogowaniu się użytkownika
                };
            }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
            }
        }
    }
}
