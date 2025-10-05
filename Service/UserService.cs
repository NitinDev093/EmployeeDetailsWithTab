using EmployeeDetailsWithTab.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeDetailsWithTab.Service
{
    public class UserService
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["my_con"].ConnectionString;

        public int InsertUserData(InsertSignDataModel data)
        {
            int insertedId = 0;
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertUser", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                cmd.Parameters.AddWithValue("@ConfirmPassword", data.ConfirmPassword);
                cmd.Parameters.AddWithValue("@MobileNumber", data.MobileNumber);
                sqlcon.Open();
                object result = cmd.ExecuteScalar();
                insertedId = Convert.ToInt32(result);
            }
            return insertedId;
        }
        public DataTable UserSignInData(String password,string email)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("sp_signInUser", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

    }
}
    
