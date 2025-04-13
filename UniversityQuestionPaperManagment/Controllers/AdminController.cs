using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityQuestionPaperManagment.Models;

namespace UniversityQuestionPaperManagment.Controllers
{
    public class AdminController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        // GET: Admin
        public ActionResult Dashboard()
        {
            //call Dashboard function to set the data dynimically
            var dashboardData = GetDashboardData();

            // Passing data to the view
            ViewBag.TotalUsers = dashboardData.TotalUsers;
            ViewBag.TotalCourses = dashboardData.TotalCourses;
            ViewBag.TotalDepartments = dashboardData.TotalDepartments;
            ViewBag.TotalTeachers = dashboardData.TotalTeachers;

            return View();
        }

        //Funtion to get Dashboard data of return type object
        private DashboardData GetDashboardData()
        {
            //setting sql connection
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //pass SP Name
                using (SqlCommand cmd = new SqlCommand("sp_GetDashboardData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //reading the data which return from SP
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Initialize a data object to store results
                        var dashboardData = new DashboardData();

                        // Read the first result set (TotalUsers)
                        if (reader.Read())
                        {
                            dashboardData.TotalUsers = reader.GetInt32(0);
                        }

                        // Move to the next result set (TotalCourses)
                        if (reader.NextResult() && reader.Read())
                        {
                            dashboardData.TotalCourses = reader.GetInt32(0);
                        }

                        // Move to the next result set (TotalDepartments)
                        if (reader.NextResult() && reader.Read())
                        {
                            dashboardData.TotalDepartments = reader.GetInt32(0);
                        }

                        // Move to the next result set (TotalTeachers)
                        if (reader.NextResult() && reader.Read())
                        {
                            dashboardData.TotalTeachers = reader.GetInt32(0);
                        }

                        return dashboardData;
                    }
                }
            }
        }

    }
}