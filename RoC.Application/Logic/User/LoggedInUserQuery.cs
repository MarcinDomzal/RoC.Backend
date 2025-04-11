using EFCoreSecondLevelCacheInterceptor;
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
    public static class LoggedInUserQuery
    {
        public class Request : IRequest<Result>
        {
        }

        public class Result
        {
            public required string Email { get; set; }
        }

        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {
            private readonly IAuthenticationDataProvider _authenticationDataProvider;

            public Handler(ICurrentAccountProvider currentaccountProvider, 
                IApplicationDbContext applicationDbContext,
                IAuthenticationDataProvider authenticationDataProvider) : base(currentaccountProvider, applicationDbContext) 
            {
                _authenticationDataProvider = authenticationDataProvider;
            }



            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {

                var UserId = _authenticationDataProvider.GetUserId();
               
                if(UserId.HasValue)
                {
                    var user = await _applicationDbContext.Users.Cacheable().FirstOrDefaultAsync(u => u.Id == UserId.Value);
                    if ( user != null)
                    {
                        return new Result()
                        { 
                            Email = user.Email
                        };
                    }
                }

                throw new UnauthorizedAccessException();
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
