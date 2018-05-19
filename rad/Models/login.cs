namespace rad.Models
{
    public class login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class staffavilability
    {
        public int doubles { get; set; }
        public int single { get; set; }
    }
    public class result
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; }
        public int roomid { get; set; }
        public string  resrvationid { get; set; }
    }
}