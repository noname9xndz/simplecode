using FluentValidation;
using Weapsy.Domain.Menus.Commands;
using System;
using Weapsy.Cqrs.Commands;
using Weapsy.Cqrs.Domain;

namespace Weapsy.Domain.Menus.Handlers
{
    public class AddMenuItemHandler : ICommandHandlerWithAggregate<AddMenuItem>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IValidator<AddMenuItem> _validator;

        public AddMenuItemHandler(IMenuRepository menuRepository,
            IValidator<AddMenuItem> validator)
        {
            _menuRepository = menuRepository;
            _validator = validator;
        }

        public IAggregateRoot Handle(AddMenuItem cmd)
        {
            var menu = _menuRepository.GetById(cmd.SiteId, cmd.MenuId);

            if (menu == null)
                throw new Exception("Menu not found");

            menu.AddMenuItem(cmd, _validator);

            _menuRepository.Update(menu);

            return menu;
        }
    }
}
