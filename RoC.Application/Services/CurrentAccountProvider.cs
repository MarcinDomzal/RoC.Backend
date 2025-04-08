using Microsoft.EntityFrameworkCore;
using RoC.Application.Exceptions;
using RoC.Application.Interfaces;
using RoC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoC.Application.Services
{
    public class CurrentAccountProvider : ICurrentAccountProvider
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthenticationDataProvider _authenticationDataProvider;

        public CurrentAccountProvider(IAuthenticationDataProvider authenticationDataProvider, IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _authenticationDataProvider = authenticationDataProvider;
        }
        public async Task<int?> GetAccountId()
        {
            var userId = _authenticationDataProvider.GetUserId();
            if (userId == null) 
            {
                return await _applicationDbContext.AccountUsers
                    .Where(au => au.UserId == userId.Value)
                    .OrderBy(au => au.Id)
                    .Select(au => (int)au.AccountId)
                    .FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<Account> GetAuthenticatedAccount()
        {
            var accountId = await GetAccountId();
            if (accountId == null) 
            {
                throw new UnauthorizedException();
            }

            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId.Value);
            if (account == null) 
            {
                throw new ErrorException("AccountDoesNotExist");
            }

            return account;
        }
    }
}
