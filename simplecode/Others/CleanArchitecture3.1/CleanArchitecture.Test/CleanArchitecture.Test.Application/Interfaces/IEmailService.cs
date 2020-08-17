using CleanArchitecture.Test.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
