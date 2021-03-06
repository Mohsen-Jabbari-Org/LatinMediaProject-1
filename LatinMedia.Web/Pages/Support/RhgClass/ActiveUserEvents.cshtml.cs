using System;
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

namespace LatinMedia.Web.Pages.Support.RhgClass
{
    public class ActiveUserEventsModel : PageModel
    {
        private IUserService _userService;
        private ICourseService _courseService;

        public ActiveUserEventsModel(IUserService userService, ICourseService courseService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        [BindProperty]
        public List<ShowUserEventsViewModel> ShowUserEventsViewModels { get; set; }
        public void OnGet(string id)
        {
            string mobile = id.Substring(0, 11).ToString();
            string Course = id.Substring(11, id.Length - mobile.Length).ToString();
            ViewData["Course"] = Course;
            int userId = _userService.GetUserByMobile(mobile).UserId;
            ShowUserEventsViewModels = _courseService.GetEventOfUsers(userId);
        }
    }
}