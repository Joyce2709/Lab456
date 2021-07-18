using Lab456.DTOs;
using Lab456.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Web.Helpers;
namespace Lab456.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {

            var userId = User.Identity.GetUserId();
            var attendance = new Attendance
            {
                CourseID = attendanceDto.CourseId,
                AttendeeId = userId
            };

            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseID == attendanceDto.CourseId))
            {

                //_dbContext.Attendances.Remove(attendance);
                _dbContext.Entry(attendance).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.SaveChanges();
                return Json(new { isFollow = false });
            }
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Json(new { isFollow = true });
        }
    }
}
