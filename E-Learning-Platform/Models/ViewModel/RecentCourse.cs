using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class RecentCourse
    {
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public decimal CourseFee { get; set; }
        public string Duration { get; set; }
        public string Instructor {  get; set; }

    }
}