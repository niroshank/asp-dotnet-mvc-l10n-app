
using rad.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rad.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index()
        {
            ViewBag.message = Session["message"];
            ViewBag.loginerror = Session["loginerror"];
            Session.Remove("message");
            Session.Remove("loginerror");
            return View();
        }
        public ActionResult feedback(string message, string email)
        {

            SqlConnection con = new dbconnection().openconnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into [feedbacks] (email,message) values('" + email + "','" + message + "')";
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {

                Session["message"] = "Your message is Successfully sent";
            }
            else
            {
                Session["message"] = "Sorry Your message is not sent";
            }
            return RedirectToAction("index", "home");

        }
        public ActionResult image_Gallery()
        {
            return View();
        }
        public ActionResult messageviews()
        {

            SqlConnection con = new dbconnection().openconnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from [feedbacks]";
            SqlDataReader dr = cmd.ExecuteReader();
            List<rad.Models.home.feedbacks> mess = new List<rad.Models.home.feedbacks>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    mess.Add(new home.feedbacks
                    {
                        message = dr["message"].ToString(),
                        email = dr["email"].ToString(),

                        feedbackid = int.Parse(dr["feedbackid"].ToString())
                    }
                    );
                    Session["feedback"] = mess;
                   
                }
            }
            return View();
        }
    }
}