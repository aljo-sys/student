using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using student.Models;
//using StudentApp.Models;
//using System.Linq;
//using System.Web.Mvc;

namespace Student.Controllers
{
    public class StudentController : Controller
    {
        private StudentContext db = new StudentContext();
        private string connectionString = "Server=WIN-SDOFN341QVO;Database=StudentDb;User Id=sa;Password=,rhsm098";
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string name, string email, DateTime dateOfBirth, string Grade, string classes)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO tbl_student (Name, Email, Dob,Grade,class) VALUES (@Name, @Email, @Dob,@Grade,@class)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Dob", dateOfBirth);
                        cmd.Parameters.AddWithValue("@Grade", Grade);
                        cmd.Parameters.AddWithValue("@class", classes);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Index()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Email, Dob, Grade, class FROM tbl_student";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["Dob"]),
                            Grade = reader["Grade"].ToString(),
                            classes = reader["class"].ToString()
                        };
                        students.Add(student);
                    }
                    conn.Close();
                }
            }
            return View(students);
        }
    }


        public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Grade { get; set; }
        public string classes { get; set; }
    }
}
