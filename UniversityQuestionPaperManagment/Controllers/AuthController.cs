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
    public class AuthController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

        //Simple Login view which open Login Html page
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //After click on Login button this function call
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            try
            {
                //setting param for Stored Procedure
                SqlParameter[] param = {
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@Password", model.Password)
            };
                //Executing SP
                DataTable dt = DBHelper.ExecuteSelect("sp_LoginUser", param, connectionString);

                if (dt.Rows.Count > 0)
                {
                    //setting Session for Login
                    Session["UserId"] = dt.Rows[0]["Id"];
                    Session["FullName"] = dt.Rows[0]["FullName"];
                    string role = dt.Rows[0]["RoleName"].ToString();
                    Session["Role"] = role;

                    //check Role of user and Redirect to the page accordingly
                    if (role == "Admin") return RedirectToAction("Dashboard", "Admin");
                    if (role == "Teacher") return RedirectToAction("Dashboard", "Teacher");
                    if (role == "Moderator") return RedirectToAction("Dashboard", "Moderator");
                }
                //if no credentials found in the Database the set this error
                ViewBag.Error = "Invalid Email or Password";
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public ActionResult Logout()
        {
            //clear the current session on logout event
            Session.Clear(); // Removes all session data
            Session.Abandon(); // Ends the session
            return RedirectToAction("Login", "Auth"); // Redirect to login
        }
    }
}