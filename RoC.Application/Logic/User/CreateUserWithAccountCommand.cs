using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public static class CreateUserWithAccountCommand
    {
        public class Request : IRequest<Result>
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        public class Result
        {
            public required int UserId { get; set; }
        }

        public class Handler : BaseCommandHandler, IRequestHandler<Request, Result>
        {
            private readonly IPasswordManager _passwordManager;
            
            public Handler(ICurrentAccountProvider currentaccountProvider, IApplicationDbContext applicationDbContext, IPasswordManager passwordManager) : base(currentaccountProvider, applicationDbContext) 
            {
                _passwordManager = passwordManager;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var userExists = await _applicationDbContext.Users.AnyAsync(u => u.Email == request.Email);
                if (userExists)
                {
                    throw new Exception("AccountWithThisMailAlreadyExists");
                }

                var uctNow = DateTime.UtcNow;
                var user = new Domain.Entities.User()
                {
                    RegisterDate = uctNow,
                    Email = request.Email,
                    HashedPassword = ""
                };

                user.HashedPassword = _passwordManager.HashPassword(request.Password);
                _applicationDbContext.Users.Add(user);

                var account = new Domain.Entities.Account()
                {
                    Name = request.Email,
                    CreateDate  = uctNow
                    
                };

                _applicationDbContext.Accounts.Add(account);

                var accountUser = new AccountUser()
                {
                    Account = account,
                    User = user,
                };

                _applicationDbContext.AccountUsers.Add(accountUser);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return new Result()
                { 
                    UserId = user.Id 
                };
            }
        }
    }
}
