using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Domain.Services.Interface
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
