using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace WebApplication1.Pages.Signup
{
    public class SignupModel : PageModel
    {
        public List<UsersInfo> listUsers = new List<UsersInfo>();
        public UsersInfo usersInfo = new UsersInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnPost()
        {

            usersInfo.name = Request.Form["name"];
            usersInfo.type = "Client";
            usersInfo.email = Request.Form["email"];
            usersInfo.phone = Request.Form["phone"];
            usersInfo.pass = Request.Form["password1"];
            String passtest = Request.Form["password2"];

            if (usersInfo.name.Length == 0 || usersInfo.email.Length == 0 ||
                 usersInfo.phone.Length == 0 || usersInfo.pass.Length == 0
                 || passtest.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            if (usersInfo.pass != passtest)
            {
                errorMessage = "Password doesnt match : please make sure the passwords are the same";
                return;
            }
            if (usersInfo.pass.Length < 6 || passtest.Length < 6)
                {
                errorMessage = "password must be 6 characters or above";
                return;
            }
            if (usersInfo.pass.Length > 30 || passtest.Length > 30)
            {
                errorMessage = "password too long";
                return;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(usersInfo.pass);

        

            // save the new user into database

            try
                {
                String connectionString = "Data Source=DESKTOP-PVICC2P;Initial Catalog=TestDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users" +
                                 "(type,name,email,phone,pass)VALUES " +
                                 "(@type,@name,@email,@phone,@pass);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@type", usersInfo.type);
                        command.Parameters.AddWithValue("@name", usersInfo.name);
                        command.Parameters.AddWithValue("@email", usersInfo.email);
                        command.Parameters.AddWithValue("@phone", usersInfo.phone);
                        command.Parameters.AddWithValue("@pass", passwordHash);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            usersInfo.name = ""; usersInfo.email = ""; usersInfo.phone = "";
                usersInfo.type  = ""; usersInfo.pass = "";
            successMessage = "Welcome";
              
            Response.Redirect("/Userspace/Index");
           
        }
        public class UsersInfo
        {
            public String id;
            public String type;
            public String name;
            public String email;
            public String phone;
            public String pass;               
            public String created_at;
        }
    }
}

