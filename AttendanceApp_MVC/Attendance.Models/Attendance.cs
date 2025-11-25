
namespace AttendanceBook.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string  AttendanceReason{ get; set; }

       
    }
}
