using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rad.Models
{
    public class avilability

    {
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
        public int rooms { get; set; }
        public int adults { get; set; }
        public int childs { get; set; }
    }
    public class reservation
    {
         public String firstname { get; set; }
        public String lastname { get; set; }
        public String address { get; set; }
        public String identificationtype { get; set; }
        public String identify { get; set; }
        public int telephone { get; set; }
        public String email { get; set; }
        public int creditcardno { get; set; }
    }


    public class roomlist
    {
        public int roomid { get; set; }
        public int roomno { get; set; }
        public string roomtype { get; set; }

    }

}