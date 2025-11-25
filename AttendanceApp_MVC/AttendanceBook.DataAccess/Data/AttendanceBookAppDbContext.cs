

using AttendanceBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceBook.DataAccess.Data
{
    public class AttendanceBookAppDbContext : DbContext
    {
        public AttendanceBookAppDbContext(DbContextOptions options) : base(options)
        {
        }
        //protected AttendanceBookAppDbContext()
        //{
        //}
        public DbSet<Attendance> Attendances { get; set; }
    }
}
