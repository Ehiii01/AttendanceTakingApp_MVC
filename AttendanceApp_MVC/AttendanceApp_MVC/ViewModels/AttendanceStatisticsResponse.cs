using AttendanceBook.Models;

namespace AttendanceBookWeb.ViewModels;

public class AttendanceStatisticsResponse
{
    public int TotalDailyAttendance { get; set; }
    public int TotalCurrentlyCheckedInUsers { get; set; }
    public int TotalCurrentlyCheckedOutUsers { get; set; }
    public List<Attendance> Top5RecentlyCheckedInUsers { get; set; } = new List<Attendance>();
    public List<Attendance> TotalAttendanceForCurrentYear { get; set; } = new List<Attendance>();
    public AttendanceStatistics Statistics { get; set; } 
    public List<Attendance> AttendanceWithIssues { get; set; } 

}




public class AttendanceStatistics
{
    public int TotalAttendanceForCurrentYear { get; set; }
    public int TotalAttendance{ get; set; }
    public int TotalAttendanceForCurrentMonth{ get; set; }
    public string MonthWithTheHighestAttendance{ get; set; }
    public int AttendanceWithIssues{ get; set; }
}