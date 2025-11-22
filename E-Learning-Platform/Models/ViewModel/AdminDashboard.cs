using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class AdminDashboard
    {
        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalEnrollments { get; set; }
    }
}