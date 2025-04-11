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

namespace RoC.Application.Logic.Account
{
    public static class CurrentAccountQuery
    {
        public class Request : IRequest<Result>
        {
        }

        public class Result
        {
            public required string Name { get; set; }
        }

        public class Handler : BaseQueryHandler, IRequestHandler<Request, Result>
        {

            public Handler(ICurrentAccountProvider currentaccountProvider, 
                IApplicationDbContext applicationDbContext) : base(currentaccountProvider, applicationDbContext) 
            {
            }



            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {

                var account = await _currentAccountProvider.GetAuthenticatedAccount();
                return new Result()
                {
                    Name = account.Name
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
