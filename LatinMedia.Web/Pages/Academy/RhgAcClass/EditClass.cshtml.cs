﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LatinMedia.Core.Convertors;
using LatinMedia.Core.Security;
using LatinMedia.Core.Services.Interfaces;
using LatinMedia.DataLayer.Entities.Course;
using LatinMedia.DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LatinMedia.Web.Pages.Academy.RhgAcClass
{
    public class EditClassModel : PageModel
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public EditClassModel(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }


        [BindProperty]
        public Course Course { get; set; }
        public State State { get; set; }
        public City City { get; set; }
        public LatinMedia.DataLayer.Entities.User.Academy Academy { get; set; }

        public void OnGet(int id)
        {
            

            Course = _courseService.GetCourseById(id);

            ViewData["MeetingUser"] = Course.CourseLatinTitle;
            ViewData["MeetingTeacher"] = Course.Language;

            var groups = _courseService.GetGroupsForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            string a = User.Identity.GetAcademyId();
            Int32 AcId = Int32.Parse(a);
            var academy = _userService.GetAcademyName(AcId);
            ViewData["Academies"] = new SelectList(academy, "Value", "Text", Course.CompanyId);

            var level = _courseService.GetLevelsForManageCourse();
            ViewData["CourseLevel"] = new SelectList(level, "Value", "Text");

            var type = _courseService.GetCourseTypesForManageCourse();
            ViewData["CourseType"] = new SelectList(type, "Value", "Text");

            var teacher = _courseService.GetTeachersFromUsersToManageCourse(Course.CompanyId);
            ViewData["CourseTeacher"] = new SelectList(teacher, "Value", "Text");

            var users = _courseService.GetUsersFromUsersToManageCourse();
            ViewData["CourseUsers"] = new SelectList(users, "Value", "Text");

            var company = _courseService.GetCompaniesForManageCourse();
            ViewData["CourseCompany"] = new SelectList(company, "Value", "Text");

            var validTimes = _courseService.GetValidTimesForManageCourse(AcId);
            ViewData["ValidTimes"] = new SelectList(validTimes, "Value", "Text");
        }

        public IActionResult OnPost(IFormFile courseFile, IFormFile imgCourseUp, string stDate = "", string fhDate = "", int AcademyId = 0)
        {
            string a = User.Identity.GetAcademyId();
            Int32 AcId = Int32.Parse(a);
            PersianCalendar pc = new PersianCalendar();
            PersianCalendar pcEnd = new PersianCalendar();
            PersianCalendar pcStart = new PersianCalendar();
            PersianCalendar pcFinish = new PersianCalendar();
            // 12/20/1397
            if (!string.IsNullOrEmpty(stDate) && !string.IsNullOrEmpty(fhDate))
            {
                string time = _courseService.GetStartTimes(Course.VTA_Id);
                string endtime = _courseService.GetEndTimes(Course.VTA_Id);
                string tDate = FixedText.FixedTxt(stDate);
                string hDate = FixedText.FixedTxt(fhDate);
                string[] std = tDate.Split("/");
                string[] htd = hDate.Split("/");
                DateTime dt = new DateTime(int.Parse(std[2]), int.Parse(std[0]), int.Parse(std[1]), int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(3, 2)), 0, pc);
                DateTime dtEnd = new DateTime(int.Parse(htd[2]), int.Parse(htd[0]), int.Parse(htd[1]), int.Parse(endtime.Substring(0, 2)), int.Parse(endtime.Substring(3, 2)), 0, pcEnd);
                Course.CreateDate = Convert.ToDateTime(dt.ToString(CultureInfo.InvariantCulture));
                Course.EndDate = Convert.ToDateTime(dtEnd.ToString(CultureInfo.InvariantCulture));
                DateTime dtstart = new DateTime(int.Parse(std[2]), int.Parse(std[0]), int.Parse(std[1]), pcStart);
                DateTime dtFinish = new DateTime(int.Parse(htd[2]), int.Parse(htd[0]), int.Parse(htd[1]), pcFinish);
                if (DateTime.Compare(Convert.ToDateTime(dtstart.ToString(CultureInfo.InvariantCulture)),
                    Convert.ToDateTime(dtFinish.ToString(CultureInfo.InvariantCulture))) == 0)
                {
                    Course.CourseFaTitle = string.Empty;
                    Course.CourseDescription = string.Empty;
                    Course.CourseFaTitle = _userService.GetAcademy(AcId).AcademyFullName + " " + Course.CreateDate.ToNewShamsi() + " " +
                    _courseService.GetGroupNameForAdmin(Course.GroupId) + " از ساعت " + time + " تا ساعت " + endtime;

                    Course.CourseDescription = Course.CourseFaTitle;
                }
                else
                {
                    Course.CourseFaTitle = string.Empty;
                    Course.CourseDescription = string.Empty;
                    Course.CourseFaTitle = _userService.GetAcademy(AcId).AcademyFullName + " " + Course.CreateDate.ToNewShamsi() + " الی " +
                        Course.EndDate.ToNewShamsi() + " " + _courseService.GetGroupNameForAdmin(Course.GroupId) +
                        " از ساعت " + time + " تا ساعت " + endtime;

                    Course.CourseDescription = Course.CourseFaTitle;
                }
            }

            else
            {
                Course.CreateDate = DateTime.Now;
                Course.EndDate = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                var groups = _courseService.GetGroupsForManageCourse();
                ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

                var subGroups = _courseService.GetSubGroupsForManageCourse(int.Parse(groups.First().Value));
                ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Course.SubGroupId ?? 0);

                var secondSubGroups = _courseService.GetSecondSubGroupsForManageCourse(int.Parse(subGroups.First().Value));
                ViewData["SecondSubGroups"] = new SelectList(secondSubGroups, "Value", "Text", Course.SecondSubGroupId ?? 0);

                //var levels = _courseService.GetLevelsForManageCourse();
                //ViewData["Levels"] = new SelectList(levels, "Value", "Text", Course.LevelId);

                //var types = _courseService.GetCourseTypesForManageCourse();
                //ViewData["Types"] = new SelectList(types, "Value", "Text", Course.TypeId);

                var teacher = _courseService.GetTeachersFromUsersToManageCourse(Course.CompanyId);
                ViewData["CourseTeacher"] = new SelectList(teacher, "Value", "Text", Course.TeacherId);

                var users = _courseService.GetUsersFromUsersToManageCourse();
                ViewData["CourseUsers"] = new SelectList(users, "Value", "Text");

                var companies = _courseService.GetCompaniesForManageCourse();
                ViewData["Companies"] = new SelectList(companies, "Value", "Text", Course.CompanyId);

                var validTimes = _courseService.GetValidTimesForManageCourse(Course.CompanyId);
                ViewData["ValidTimes"] = new SelectList(validTimes, "Value", "Text");

                return Page();
            }
            Course.DemoFileName = Course.DemoFileName;
            
            var academy = _userService.GetAcademyName(AcId);
            Course.CompanyId = AcId;
            _courseService.UpdateCourse(Course, imgCourseUp, courseFile);
            TempData["UpdateCourse"] = true;
            return RedirectToPage("Index");
        }
    }
}