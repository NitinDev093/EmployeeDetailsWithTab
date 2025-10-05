using EmployeeDetailsWithTab.Models;
using EmployeeDetailsWithTab.Service;
using EmployeeDetailsWithTab.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeDetailsWithTab.BusinessLayer
{
    public class UserBusinessLayer
    {
        UserService user=new UserService();
        public UserResponseModel<string> InsertuserSignInData(InsertSignDataModel data)
        {
            UserResponseModel<string> responnse=new UserResponseModel<string>();
            data.Password = EncodeDecodeHelper.EncodedData(data.Password);
            int userId = user.InsertUserData(data);
            if (userId > 0)
            {
                string Id = EncodeDecodeHelper.EncodedData(userId.ToString());
                EmailHelper.SendEmail(data.Email, "Registration Successfull", "Your reference Id is" + Id);
                responnse.IsSuccess= true;
                responnse.Message = "Regestration Successfull";
            }
            else
            {
                responnse.IsSuccess = false;
                responnse.Message = "Unable To Register User";
            }
            return responnse;
        }
        public UserResponseModel<string> UserSignInData(string password, string email)
        {
            UserResponseModel<string> responnse = new UserResponseModel<string>();
            password = EncodeDecodeHelper.EncodedData(password);
            DataTable data = user.UserSignInData(password,email);
            InsertSignDataModel modeldata = new InsertSignDataModel();
            string jsonData = JsonConvert.SerializeObject(data);
            modeldata = JsonConvert.DeserializeObject<List<InsertSignDataModel>>(jsonData).FirstOrDefault();
            if (modeldata != null)
            {
                string token=JWthelper.GenerateToken(modeldata);
                responnse.IsSuccess = true;
                responnse.Message = "Login Successfull";
                responnse.Data = token;
            }
            else
            {
                responnse.IsSuccess = false;
                responnse.Message = "Invalid Email or Password";
            }
            return responnse;
        }
    }
}