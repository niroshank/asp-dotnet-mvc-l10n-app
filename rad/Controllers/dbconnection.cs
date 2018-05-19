using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace rad.Models
{
    public class dbconnection
    {
        public SqlConnection openconnection( )
        {
        

                SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["hotelmanagement"].ConnectionString);
                con.Open();
          
                return con;
            
       
          
        }
    }
}