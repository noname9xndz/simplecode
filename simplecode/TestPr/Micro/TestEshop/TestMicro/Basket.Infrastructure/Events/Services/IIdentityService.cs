using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Infrastructure.Events.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();
    }
}
