using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Platform.Models.ViewModel
{
    public class AppliedTeacher
    {
        public string Name { get; set; }
        public string Qualification { get; set; }
        public string Subject { get; set; }
        public string Gmail { get; set; }
        public DateTime AppliedDate {  get; set; }
    }
}