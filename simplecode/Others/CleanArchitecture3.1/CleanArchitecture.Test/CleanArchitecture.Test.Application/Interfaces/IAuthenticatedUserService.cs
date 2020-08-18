using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Test.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
