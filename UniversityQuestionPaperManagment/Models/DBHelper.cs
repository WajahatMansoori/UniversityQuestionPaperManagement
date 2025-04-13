using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UniversityQuestionPaperManagment.Models
{
    public static class DBHelper
    {
        //private static string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public static DataTable ExecuteSelect(string procedureName, SqlParameter[] parameters, string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    
}