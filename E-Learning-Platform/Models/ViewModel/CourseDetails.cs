using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class CourseDetails
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImagePath { get; set; }

        public int Course_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duretion { get; set; }
        public Nullable<decimal> Course_Fee { get; set; }
    }
}