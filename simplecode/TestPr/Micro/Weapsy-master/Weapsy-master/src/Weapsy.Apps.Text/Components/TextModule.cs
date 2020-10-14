﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weapsy.Mvc.Components;
using Weapsy.Mvc.Context;
using Weapsy.Reporting.Pages;
using Weapsy.Apps.Text.Reporting;
using Weapsy.Apps.Text.Domain.Commands;
using System;
using Weapsy.Apps.Text.Domain;
using Weapsy.Cqrs;

namespace Weapsy.Apps.Text.Components
{
    [ViewComponent(Name = "TextModule")]
    public class TextViewComponent : BaseViewComponent
    {
        private readonly IContextService _contextService;
        private readonly IDispatcher _dispatcher;

        public TextViewComponent(IContextService contextService,
            IDispatcher dispatcher)
            : base(contextService)
        {
            _contextService = contextService;
            _dispatcher = dispatcher;
        }

        public async Task<IViewComponentResult> InvokeAsync(ModuleModel model)
        {
            var content = await _dispatcher.GetResultAsync<GetContent, string>(new GetContent
            {
                ModuleId = model.Id,
                LanguageId = _contextService.GetCurrentLanguageInfo().Id
            });

            if (content == null)
            {
                const string defaultContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

                await _dispatcher.SendAndPublishAsync<CreateTextModule, TextModule>(new CreateTextModule
                {
                    SiteId = SiteId,
                    Id = Guid.NewGuid(),
                    ModuleId = model.Id,
                    Content = defaultContent
                });

                content = defaultContent;
            }

            return View("~/Apps/Weapsy.Apps.Text/Views/Shared/Components/TextModule/Default.cshtml", content);
        }
    }
}
