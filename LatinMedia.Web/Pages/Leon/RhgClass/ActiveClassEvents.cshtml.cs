﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigBlueButtonAPI.Core;
using LatinMedia.Core.Security;
using LatinMedia.Core.Services.Interfaces;
using LatinMedia.Core.ViewModels;
using LatinMedia.DataLayer.Entities.Course;
using LatinMedia.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LatinMedia.Web.Pages.Leon.RhgClass
{
    [UserRoleChecker]
    [PermissionChecker(38)]
    public class ActiveClassEventsModel : PageModel
    {
        private IUserService _userService;
        private ICourseService _courseService;

        public ActiveClassEventsModel(IUserService userService, ICourseService courseService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        [BindProperty]
        public List<ShowClassEventsViewModel> ShowClassEventsViewModels { get; set; }
        public void OnGet(int id)
        {
            ShowClassEventsViewModels = _courseService.GetEventOfClass(id);
        }
    }
}