using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDetailsWithTab.Models
{
    public class UserResponseModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}