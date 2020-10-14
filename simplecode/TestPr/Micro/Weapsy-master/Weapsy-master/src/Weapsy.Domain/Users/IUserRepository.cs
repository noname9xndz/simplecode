﻿using System;
using System.Threading.Tasks;

namespace Weapsy.Domain.Users
{
    public interface IUserRepository
    {
        User GetById(Guid id);
        User GetByEmail(string email);
        User GetByUserName(string userName);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task AddToRoleAsync(Guid id, string roleName);
        Task RemoveFromRoleAsync(Guid id, string roleName);
    }
}
