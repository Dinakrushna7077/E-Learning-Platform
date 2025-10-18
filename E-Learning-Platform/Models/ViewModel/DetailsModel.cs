using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class DetailsModel
    {
        public string Qualification { get; set; }
        public string SubjectExpertise { get; set; }
        public string Gender { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public int CourseId { get; set; }

    }
}