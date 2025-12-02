
namespace AttendanceBook.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool IsCheckOut{ get; set; } 
        public string  AttendanceReason{ get; set; }
        public string ImageUrl { get; set; }


    }
}
