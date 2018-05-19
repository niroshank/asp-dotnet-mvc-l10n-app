using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace rad.Controllers
{
    public class AvilabilityController : Controller
    {
        private int ok;

        // GET: Avilability
        public ActionResult Index()
        {
            // ViewBag.avimsg= "sorry all Rooms are reserved";
            ViewBag.avimsg = Session["notok"];

           // ViewBag.date = Session["d"];
           Session.Remove("notok");

            return View();
        }
        public ActionResult checkroom()
        {
            ViewBag.singleroom = Session["singleroom"];
            ViewBag.doubleroom = Session["doubleroom"];
            

            return View();
        }
        public ActionResult reservation()
        {
            ViewBag.max = Session["max"];
            
            return View();
        }
    
        [HttpPost]
        public ActionResult staffreservate(DateTime checkin,DateTime checkout)
        {
            string ckin = checkin.ToString("yyyy-MM-dd");
            string ckout = checkout.ToString("yyyy-MM-dd");
            SqlConnection con = new Models.dbconnection().openconnection();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = " select roomid from [rooms] where NOT roomid IN(select roomid from [reservation] where (checkin BETWEEN '" + ckin + "' AND '" + ckout + "') OR(checkout BETWEEN '" + ckin + "' AND '" + ckout + "'))";
            SqlDataReader dr = cmd1.ExecuteReader();

            int singler = 0;
            int counter = 0;
            int doubler = 0;
            while (dr.Read())
            {
                if (Convert.ToInt32(dr["roomid"].ToString()) <= 50)
                {
                    singler++;
                }
                else
                {
                    doubler++;
                }
                counter++;
            }
            Session["ckin"] = ckin;
            Session["ckot"] = ckout;
            Session["singleroom"] = singler.ToString();
            Session["doubleroom"] = doubler.ToString();
            return RedirectToAction("dashboardother", "login");
            return View();
        }
        [HttpPost]
        public ActionResult res(string firstname, string rooomtype, string lastname,
            string creditcard, string address, string identificationtype, string identify, string telephone, string email,int noofrooms)
        {
           
            int c = 1;
              string roomtype = ((Session["roomtype"]).ToString());
         
              
              string ckin = Session["ckin"].ToString();
              string ckout = Session["ckot"].ToString();
           
           SqlConnection con = new Models.dbconnection().openconnection();
            SqlCommand cmd1 = con.CreateCommand();
                  cmd1.CommandType = System.Data.CommandType.Text;
               cmd1.CommandText = " select * from [rooms] where NOT roomid IN(select roomid from [reservation] where (checkin BETWEEN '" + ckin + "' AND '" + ckout + "') OR(checkout BETWEEN '" + ckin + "' AND '" + ckout + "'))";
                 SqlDataReader dr1 = cmd1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr1);
            dr1.Close();
            int i = 0;
            while (dt.Rows.Count>i)
            {
           int roomid= int.Parse ( dt.Rows[i][1].ToString());

                if ((dt.Rows[i][2].ToString())== roomtype && noofrooms>=c)
                {

                   
                      SqlCommand cmd = con.CreateCommand();
                     cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO[reservation](firstname,lastname,identificationtype,identifynumber,addresses,telephone,email,creditcardno,checkin,checkout,roomid) VALUES('"+firstname+"','"+lastname+"','"+identificationtype+"','"+identify+"','"+address+"','"+telephone+"','"+email+"','"+creditcard+"','"+ ckin + "','"+ ckout+ "','"+roomid+"')";
                         int a = cmd.ExecuteNonQuery();

                  c++;

                }
                i++;
            }
        
            con.Close();
            return RedirectToAction("index", "Avilability");
           
            
        }
        
        [HttpPost]
        public ActionResult reservate(int childs, DateTime checkin, DateTime checkout, int rooms, int adults)
        {
            

            string ckin =checkin.ToString("yyyy-MM-dd");
            string ckout =checkout.ToString("yyyy-MM-dd");
            SqlConnection con = new Models.dbconnection().openconnection();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = " select roomid from [rooms] where NOT roomid IN(select roomid from [reservation] where (checkin BETWEEN '" + ckin + "' AND '" + ckout + "') OR(checkout BETWEEN '" + ckin + "' AND '" + ckout + "'))";
            SqlDataReader dr = cmd1.ExecuteReader();
           
            int singler=0;
            int counter = 0;
            int doubler = 0;
            while (dr.Read())
            {
                  if (Convert.ToInt32(dr["roomid"].ToString()) <= 50)
                    {
                    singler++;
                }
                else
                {
                    doubler++;
                }
                counter++;
            }

            if (counter >= rooms)
            {
               
                Session["ckin"] = ckin;
                Session["ckot"] = ckout;
                Session["singleroom"] = singler.ToString();
                Session["doubleroom"] = doubler.ToString();
              //  Session["numberofrooms"] = rooms;

               
                    return RedirectToAction("checkroom", "Avilability");
                
            }
            else
            {
                Session["notok"] = "ok";
                return RedirectToAction("index", "Avilability");
            }
            con.Close();
            return View();
        }
public ActionResult singleroomconfirm()
        {
           Session.Remove("roomtype");
            Session["roomtype"] = "single";
            Session["max"] = (Session["singleroom"].ToString());
            return RedirectToAction("reservation");
            return View();
        }

        public ActionResult doubleroomconfirm()
        {
            Session.Remove("roomtype");
            Session["max"] = (Session["doubleroom"].ToString());
            Session["roomtype"] = "double";
            return RedirectToAction("reservation");
            return View();
        }
       public ActionResult cancelreservation(int reservationid, string Identificationno)
        {
            SqlConnection con= new Models.dbconnection().openconnection();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            if (!(reservationid == 0))
            {
                cmd1.CommandText = "delete from [reservation] where reservationid='" + reservationid + "'";
                int a = cmd1.ExecuteNonQuery();
                if (a > 0)
                {
                    Session["delete"] = "Reservation Deleted sucessfully ";

                }
                else
                {
                    Session["errordelete"] = "Sorry Your entered details are not valid";

                }
            }
            else
            {
                cmd1.CommandText = "delete from [reservation] where identifynumber='" + Identificationno + "'";
                int a = cmd1.ExecuteNonQuery();
                if (a > 0)
                {
                    Session["delete"] = "Reservation Deleted sucessfully ";

                }
                else {
                    Session["errordelete"] = "Sorry Your entered details are not valid";
                }
            }
            return RedirectToAction("dashboardother", "login");
            return View();
        }





        public ActionResult addreservation(string firstname,  string lastname,
            string creditcard, string address, string identificationtype, string identify, string telephone, string email, int noofroomsdouble,int noofroomssingle)
        {
            int c = 1;
            int d= 1;
            string username = (Session["UserName"].ToString());

            string ckin = Session["ckin"].ToString();
            string ckout = Session["ckot"].ToString();

            SqlConnection con = new Models.dbconnection().openconnection();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = " select * from [rooms] where NOT roomid IN(select roomid from [reservation] where (checkin BETWEEN '" + ckin + "' AND '" + ckout + "') OR(checkout BETWEEN '" + ckin + "' AND '" + ckout + "'))";
            SqlDataReader dr1 = cmd1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr1);
            dr1.Close();
            int i = 0;
            int a = 0;
            int b = 0;
            while (dt.Rows.Count > i)
            {
                int roomid = int.Parse(dt.Rows[i][0].ToString());
                if(noofroomssingle== 0) { } else {
                if ((dt.Rows[i][2].ToString()) =="single" && noofroomssingle >= c)
                {

                    // SqlConnection con = new Models.dbconnection().openconnection();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO[reservation](firstname,lastname,identificationtype,identifynumber,addresses,telephone,email,creditcardno,checkin,checkout,roomid,username) VALUES('" + firstname + "','" + lastname + "','" + identificationtype + "','" + identify + "','" + address + "','" + telephone + "','" + email + "','" + creditcard + "','" + ckin + "','" + ckout + "','" + roomid + "','"+ username + "')";
                    int x= cmd.ExecuteNonQuery();
                        a = a + x;
                    c++;

                    }
                }

                if (noofroomsdouble == 0) { }
                else
                {
                    if ((dt.Rows[i][2].ToString()) == "double" && noofroomssingle >= d)
                    {
                        
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO[reservation](firstname,lastname,identificationtype,identifynumber,addresses,telephone,email,creditcardno,checkin,checkout,roomid,username) VALUES('" + firstname + "','" + lastname + "','" + identificationtype + "','" + identify + "','" + address + "','" + telephone + "','" + email + "','" + creditcard + "','" + ckin + "','" + ckout + "','" + roomid + "','" + username + "')";
   int f = cmd.ExecuteNonQuery();
                        b = b + a;
                        d++;

                    }
                }
                i++;
            }
         
            con.Close();
            int all = noofroomsdouble + noofroomssingle;
            if (all == (a + b))
            {
                Session["reservationadd"] = "Reservation Added Successfully";
            }
            else
            {
                Session["reservationnotadd"] = "Reservation Adding Failed ";
            }
            return RedirectToAction("dashboardother", "login");
            return View();
        }
    
    }

}