﻿using LatinMedia.DataLayer.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LatinMedia.DataLayer.Entities.Course
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string GroupTitle { get; set; }

        [Display(Name = "توضیحات گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string Description { get; set; }

        [Display(Name = "تصویر گروه")]
        [MaxLength(100)]
        public string GroupImageName { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<CourseGroup> CourseGroups { get; set; }

        [InverseProperty("CourseGroup")]
        public List<Course> Courses { get; set; }

        [InverseProperty("SubGroup")]
        public List<Course> SubGroupCourses { get; set; }

        [InverseProperty("SecondSubGroup")]
        public List<Course> SecondSubGroupCourses { get; set; }
        public List<TeacherGroup> TeacherGroup { get; set; }
        #endregion

    }
}
