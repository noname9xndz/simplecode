﻿using System.Threading.Tasks;
using Weapsy.Reporting.Users.Queries;
using Weapsy.Reporting.Users;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Weapsy.Data.Entities;
using Weapsy.Data.TempIdentity;
using Weapsy.Cqrs.Queries;

namespace Weapsy.Data.Reporting.Users
{
    public class GetUserRolesViewModelHandler : IQueryHandlerAsync<GetUserRolesViewModel, UserRolesViewModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public GetUserRolesViewModelHandler(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserRolesViewModel> RetrieveAsync(GetUserRolesViewModel query)
        {
            var user = await _userManager.FindByIdAsync(query.Id.ToString());

            if (user == null)
                return null;

            var userRoles = await _userManager.GetRolesAsync(user);
            var availableRoles = _roleManager.Roles.Where(x => !userRoles.Contains(x.Name)).ToList();

            var model = new UserRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                AvailableRoles = availableRoles.OrderBy(x => x.Name).Select(x => x.Name).ToList(),
                UserRoles = userRoles.OrderBy(x => x).ToList()
            };

            return model;
        }
    }
}
