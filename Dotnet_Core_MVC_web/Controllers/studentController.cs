using Dotnet_Core_MVC_web.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Core_MVC_web.Controllers
{
    public class studentController : Controller
    {
        private readonly IConfiguration configuration;

        public studentController(IConfiguration config)
        {
            this.configuration = config;
        }
        // GET: student
        public ActionResult Index()
        {
            string constr = configuration.GetConnectionString("DevConnection");

            List<student> list = new List<student>();
            SqlConnection conn = new SqlConnection(constr);
            string query = "sp_fetch";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                list.Add(new student
                {
                    id = Convert.ToInt32(sdr["id"]),
                    name = sdr["name"].ToString(),
                    course = sdr["course"].ToString(),
                    fees = Convert.ToInt32(sdr["fees"])

                });
            }
            conn.Close();

            return View(list);
        }

        // GET: student/Details/5
        public ActionResult Details(int id)
        {
            string constr = configuration.GetConnectionString("DevConnection");

            student obj = new student();
            SqlConnection conn = new SqlConnection(constr);
            string query = "sp_details " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                obj = new student
                {
                    id = Convert.ToInt32(sdr["id"]),
                    name = sdr["name"].ToString(),
                    course = sdr["course"].ToString(),
                    fees = Convert.ToInt32(sdr["fees"])

                };
            }
            conn.Close();

            return View(obj);
        }

        // GET: student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(student studentobj)
        {
            try
            {
                string constr = configuration.GetConnectionString("DevConnection");

                SqlConnection conn = new SqlConnection(constr);
                string query = "sp_create @name, @course, @fees";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", studentobj.name);
                cmd.Parameters.AddWithValue("@course", studentobj.course);
                cmd.Parameters.AddWithValue("@fees", studentobj.fees);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: student/Edit/5
        public ActionResult Edit(int id)
        {
            string constr = configuration.GetConnectionString("DevConnection");

            student obj = new student();
            SqlConnection conn = new SqlConnection(constr);
            string query = "sp_details " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                obj = new student
                {
                    id = Convert.ToInt32(sdr["id"]),
                    name = sdr["name"].ToString(),
                    course = sdr["course"].ToString(),
                    fees = Convert.ToInt32(sdr["fees"])

                };
            }
            conn.Close();

            return View();
        }

        // POST: student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, student studentobj)
        {
            try
            {
                string constr = configuration.GetConnectionString("DevConnection");

                SqlConnection conn = new SqlConnection(constr);
                string query = "sp_edit @id, @name, @course, @fees";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", studentobj.name);
                cmd.Parameters.AddWithValue("@course", studentobj.course);
                cmd.Parameters.AddWithValue("@fees", studentobj.fees);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: student/Delete/5
        public ActionResult Delete(int id)
        {
            string constr = configuration.GetConnectionString("DevConnection");

            student obj = new student();
            SqlConnection conn = new SqlConnection(constr);
            string query = "sp_details " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                obj = new student
                {
                    id = Convert.ToInt32(sdr["id"]),
                    name = sdr["name"].ToString(),
                    course = sdr["course"].ToString(),
                    fees = Convert.ToInt32(sdr["fees"])

                };
            }
            conn.Close();

            return View();
        }

        // POST: student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string constr = configuration.GetConnectionString("DevConnection");

                SqlConnection conn = new SqlConnection(constr);
                string query = "sp_delete @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
