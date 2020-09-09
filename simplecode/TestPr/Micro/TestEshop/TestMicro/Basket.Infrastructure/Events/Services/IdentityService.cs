using Microsoft.AspNetCore.Http;
using System;

namespace Basket.Infrastructure.Events.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            // return _context.HttpContext.User.FindFirst("sub").Value;
            return "0AA43253-AA8A-E9A6-0F88-071E37CCE1B2";
        }
    }
}
