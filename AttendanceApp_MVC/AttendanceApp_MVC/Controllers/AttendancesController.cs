
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceBook.DataAccess.Data;
using AttendanceBook.Models;
using AttendanceBookWeb.ViewModels;
using Microsoft.AspNetCore.Http;

namespace AttendanceBookWeb.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly AttendanceBookAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttendancesController(AttendanceBookAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Attendances
        public async Task<IActionResult> Index(string? fullName = null)
        {

            var attendance = new List<Attendance>();


            if (string.IsNullOrWhiteSpace(fullName))
            {
                attendance = await _context.Attendances.ToListAsync();
                return View(attendance);

            }
            attendance = await _context.Attendances
                .Where(a => EF.Functions.Like(a.FullName, $"%{fullName}%")).ToListAsync();
            if (attendance == null)
            {
                return RedirectToAction("Index");
            }
            return View(attendance);




        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Age,AttendanceReason")] AttendanceRequest request)
        {
            if (ModelState.IsValid)
            {
               
                var attendanceData = new Attendance
                {
                    FullName = request.FullName,
                    Age = request.Age,
                    AttendanceReason = request.AttendanceReason,
                    CheckIn = DateTime.Now,
                    IsCheckOut = false
                };

               attendanceData = _context.Attendances.Add(attendanceData).Entity;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(request);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Age,CheckInTime,CheckOutTime,AttendanceReason")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CheckOut(int attendanceId)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.Id == attendanceId);
            if(attendance == null)
            {
                return RedirectToAction("Index");
            }
            attendance.CheckOut = DateTime.Now;
            attendance.IsCheckOut = true;
            _context.Attendances.Update(attendance);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        //public IActionResult InsertImage(AttendanceViewModel attendanceVM, IFormFile? file)
        //{
        //    string wwwRootPath = _webHostEnvironment.WebRootPath;
        //    if (file != null) 
        //    { 
        //        string fileName = Guid.NewGuid().ToString().ToString() + Path.GetExtension(file.FileName);
        //        string imagePath = Path.Combine(wwwRootPath, fileName);
        //        if (!string.IsNullOrEmpty(attendanceVM.Attendance.ImageUrl))
        //        {
        //            var oldImagePath = Path.Combine(wwwRootPath, attendanceVM.Attendance.ImageUrl.TrimStart('\\'));
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }

        //        using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
        //        {
        //            file.CopyTo(fileStream);
        //        }
        //        attendanceVM.Attendance.ImageUrl = @"\images\imagecapture" + file;

        //        return View(attendanceVM);
        //    }
        //    return RedirectToAction($"{nameof(Index)}");
        //}
    }
}
