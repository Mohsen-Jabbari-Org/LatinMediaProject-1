using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatinMedia.Core.Security;
using LatinMedia.Core.Services.Interfaces;
using LatinMedia.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LatinMedia.Web.Pages.Leon.Users
{
    [UserRoleChecker]
    [PermissionChecker(11)]
    public class ListDeleteUsersModel : PageModel
    {
        private IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public void OnGet(int pageId = 1, int take = 5, string filterByLastName = "", string filterByMobile = "")
        {
            if (Request.Query.ContainsKey("fl"))
            {
                string s = Request.Query["fl"];
                if (s != "")
                    filterByLastName = Request.Query["fl"];
            }

            if (Request.Query.ContainsKey("fm"))
            {
                string s = Request.Query["fm"];
                if (s != "")
                    filterByMobile = Request.Query["fm"];
            }

            ViewData["FilterLastName"] = filterByLastName;
            ViewData["FilterMobile"] = filterByMobile;
            ViewData["PageID"] = (pageId - 1) * take + 1;
            UsersForAdminViewModel = _userService.GetDeleteUsers(pageId, take, filterByLastName, filterByMobile);

            if (pageId > 1 && pageId != UsersForAdminViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == UsersForAdminViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (UsersForAdminViewModel.UserCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}