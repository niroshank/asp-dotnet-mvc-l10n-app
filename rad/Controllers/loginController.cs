using rad.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.SessionState;
namespace rad.Controllers
{
    public class loginController : Controller
    {
        private int table;

        // GET: login
        public ActionResult Index()
        {
            // ViewBag. err = Session["loginerror"];
            //Session.RemoveAll();
            /// Session.Clear();
            return View();
        }

        // GET: login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                ViewBag.err = Session["loginerror"];
                Session.RemoveAll();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult dashboard(string username, string password)
        {
            return RedirectToAction("dashboardother", "login");
          /* string user = username;

            SqlConnection con = new Models.dbconnection().openconnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from [login] where  username='" + username + "'and password='" + password + "' ";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                int activation = Int32.Parse(dr["activation"].ToString());
                if (activation == '0')
                {
                    return RedirectToAction("index", "home");
                  
                }
                else
                {

                    Session["UserName"] = user;
                    return RedirectToAction("dashboardother", "login");
                }
            }
            else
            {


                Session["loginerror"] = "invalid username or password";
                return RedirectToAction("index", "home");
            }*/
            return View();



        }
        public ActionResult dashboardother()
        {
            Session["username"] = "aaa";
            if (Session["username"] == null)
            {
                return RedirectToAction("index", "home");
            }
            ViewBag.delete = Session["delete"];
            ViewBag.singlerooms = Session["singleroom"];
            ViewBag.checkreservation = Session["checkreservation "];
              ViewBag.errordelete= Session["errordelete"];
            ViewBag.reservationadd = Session["reservationadd"];
            ViewBag.reservationnotadd = Session["reservationnotadd"];
            Session.Remove("checkreservation ");
            Session.Remove("errordelete");
            Session.Remove("delete");
            Session.Remove("reservationnotadd");
            Session.Remove("reservationadd");


            Session.Remove("singleroom");
            ViewBag.doublerooms = Session["doubleroom"];
            Session.Remove("doubleroom");
            // Session["singleroom"] 
            // Session["doubleroom"]
            ViewBag.test = Session["test"];
            return View();
        }
        public ActionResult staffreservate()
        {

            return View();
        }
        public ActionResult staffcheck(string idno)
        {
            SqlConnection con = new dbconnection().openconnection();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from [reservation] where  identifynumber='" + idno + "' ";
            SqlDataReader dr = cmd.ExecuteReader();
            List<rad.Models.result> rl =new List<rad.Models.result>();
            if (dr.HasRows) { 
            while (dr.Read())
            {
               
                rl.Add(new result {
                    firstname = dr["firstname"].ToString(),
                    lastname = dr["lastname"].ToString(),
                    checkin = (DateTime.Parse(dr["checkin"].ToString())).ToShortDateString(),
                checkout = (DateTime.Parse(dr["checkout"].ToString())).ToShortDateString(),
                  roomid =int.Parse( dr["roomid"].ToString()),
                   resrvationid=((dr["reservationid"].ToString()))
                }
                );
            }
          Session["table"] = rl;
            }
            else
            {
                Session["checkreservation "] = "sorry you are entered wrong details";
            }
            return RedirectToAction("dashboardother", "login");
            
        }
  
    }
}
