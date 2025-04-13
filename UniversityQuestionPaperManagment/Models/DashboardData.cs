using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityQuestionPaperManagment.Models
{
    public class DashboardData
    {
        public int TotalUsers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalTeachers { get; set; }
    }
}