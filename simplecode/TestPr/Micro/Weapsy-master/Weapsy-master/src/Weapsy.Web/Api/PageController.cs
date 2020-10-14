﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Cqrs;
using Weapsy.Domain.Modules;
using Weapsy.Domain.Modules.Commands;
using Weapsy.Domain.Pages;
using Weapsy.Domain.Pages.Commands;
using Weapsy.Domain.Pages.Rules;
using Weapsy.Domain.Roles.DefaultRoles;
using Weapsy.Mvc.Context;
using Weapsy.Mvc.Controllers;
using Weapsy.Reporting.Pages;
using Weapsy.Reporting.Pages.Queries;
using Weapsy.Web.Api.Models;

namespace Weapsy.Web.Api
{
    [Route("api/[controller]")]
    public class PageController : BaseAdminController
    {
        private readonly IDispatcher _dispatcher;   
        private readonly IPageRules _pageRules;

        public PageController(IDispatcher dispatcher,           
            IPageRules pageRules,
            IContextService contextService)
            : base(contextService)
        {
            _dispatcher = dispatcher;
            _pageRules = pageRules;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePage model)
        {
            model.SiteId = SiteId;
            _dispatcher.SendAndPublish<CreatePage, Page>(model);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/update")]
        public IActionResult UpdateDetails([FromBody] UpdatePageDetails model)
        {
            model.SiteId = SiteId;
             _dispatcher.SendAndPublish<UpdatePageDetails, Page>(model);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/add-module")]
        public IActionResult AddModule([FromBody] AddModuleModel model)
        {
            var defaultViewRoleIds = new List<Guid> { Administrator.Id };
            var defaultEditRoleIds = new List<Guid> { Administrator.Id };
            model.SetPageModulePermissions(defaultViewRoleIds, defaultEditRoleIds);
            model.SiteId = SiteId;

            var moduleId = Guid.NewGuid();

            var createModule = new CreateModule
            {
                SiteId = model.SiteId,
                ModuleTypeId = model.ModuleTypeId,
                Id = moduleId,
                Title = model.Title
            };
            _dispatcher.SendAndPublish<CreateModule, Module>(createModule);

            var addPageModule = new AddPageModule
            {
                SiteId = model.SiteId,
                PageId = model.PageId,
                ModuleId = moduleId,
                PageModuleId = Guid.NewGuid(),
                Title = model.Title,
                Zone = model.Zone,
                SortOrder = model.SortOrder,
                PageModulePermissions = model.PageModulePermissions
            };
            _dispatcher.SendAndPublish<AddPageModule, Page>(addPageModule);

            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/remove-module")]
        public IActionResult RemoveModule([FromBody] RemoveModule model)
        {
            model.SiteId = SiteId;
            _dispatcher.SendAndPublish<RemoveModule, Page>(model);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/reorder-modules")]
        public IActionResult ReorderPageModules([FromBody] ReorderPageModules model)
        {
            model.SiteId = SiteId;
            _dispatcher.SendAndPublish<ReorderPageModules, Page>(model);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/set-permissions")]
        public IActionResult SetPermissions([FromBody] SetPagePermissions model)
        {
            model.SiteId = SiteId;
            _dispatcher.SendAndPublish<SetPagePermissions, Page>(model);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/set-module-permissions")]
        public IActionResult SetModulePermissions([FromBody] SetPageModulePermissions model)
        {
            model.SiteId = SiteId;
            _dispatcher.SendAndPublish<SetPageModulePermissions, Page>(model);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/activate")]
        public IActionResult Activate(Guid id)
        {
            _dispatcher.SendAndPublish<ActivatePage, Page>(new ActivatePage
            {
                SiteId = SiteId,
                Id = id
            });
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/hide")]
        public IActionResult Hide(Guid id)
        {
            _dispatcher.SendAndPublish<HidePage, Page>(new HidePage
            {
                SiteId = SiteId,
                Id = id
            });
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _dispatcher.SendAndPublish<DeletePage, Page>(new DeletePage
            {
                SiteId = SiteId,
                Id = id
            });
            return new NoContentResult();
        }

        [HttpGet("{name}")]
        [Route("isPageNameUnique")]
        public IActionResult IsPageNameUnique(string name)
        {
            var isPageNameUnique = _pageRules.IsPageNameUnique(SiteId, name);
            return Ok(isPageNameUnique);
        }

        [HttpGet("{name}")]
        [Route("isPageNameValid")]
        public IActionResult IsPageNameValid(string name)
        {
            var isPageNameValid = _pageRules.IsPageNameValid(name);
            return Ok(isPageNameValid);
        }

        [HttpGet("{url}")]
        [Route("isPageSlugUnique")]
        public IActionResult IsPageSlugUnique(string url)
        {
            var isPageSlugUnique = _pageRules.IsSlugUnique(SiteId, url);
            return Ok(isPageSlugUnique);
        }

        [HttpGet("{url}")]
        [Route("isPageUrlValid")]
        public IActionResult IsPageUrlValid(string url)
        {
            var isPageUrlValid = _pageRules.IsPageUrlValid(url);
            return Ok(isPageUrlValid);
        }

        [HttpGet("{url}")]
        [Route("isPageUrlReserved")]
        public IActionResult IsPageUrlReserved(string url)
        {
            var isPageUrlReserved = _pageRules.IsPageUrlReserved(url);
            return Ok(isPageUrlReserved);
        }

        [HttpGet]
        [Route("{id}/view")]
        public async Task<IActionResult> ViewById(Guid id)
        {
            var model = await _dispatcher.GetResultAsync<GetPageInfo, PageInfo>(new GetPageInfo
            {
                SiteId = SiteId,
                PageId = id
            });

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet]
        [Route("{name}/view")]
        public IActionResult ViewByName(string name)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("admin-list")]
        public async Task<IActionResult> AdminList()
        {
            var model = await _dispatcher.GetResultAsync<GetAllForAdmin, IEnumerable<PageAdminListModel>>(new GetAllForAdmin
            {
                SiteId = SiteId
            });
            return Ok(model);
        }

        [HttpGet]
        [Route("{id}/admin-edit")]
        public async Task<IActionResult> AdminEdit(Guid id)
        {
            var model = await _dispatcher.GetResultAsync<GetForAdmin, PageAdminModel>(new GetForAdmin
            {
                SiteId = SiteId,
                Id = id
            });

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
