using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class StudentListDto
    {
        public int SId { get; set; }
        public int UId { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Gmail { get; set; }
        public long Phone { get; set; }
        public string CourseTitle { get; set; }
        public string Duration { get; set; }
        public bool Status { get; set; }
        public int CreditIndex { get; set; }

    }
}