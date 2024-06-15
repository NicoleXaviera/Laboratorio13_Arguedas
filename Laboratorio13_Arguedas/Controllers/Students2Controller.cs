using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laboratorio13_Arguedas.Models;


namespace Laboratorio13_Arguedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students2Controller : ControllerBase
    {
        private readonly SchoolContext _context;

        public Students2Controller(SchoolContext context)
        {
            _context = context;
        }
        // GET: api/Students/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchStudents(string name, string lastName)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Where(s => (string.IsNullOrEmpty(name) || s.FirstName.Contains(name)) &&
                            (string.IsNullOrEmpty(lastName) || s.LastName.Contains(lastName)) && s.Activo)
                .OrderByDescending(s => s.LastName)
                .ToListAsync();

            return students;
        }

        // GET: api/Students/SearchByNameAndGrade
        [HttpGet("SearchByNameAndGrade")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchByNameAndGrade(string name, int gradeId)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .Where(s => (string.IsNullOrEmpty(name) || s.FirstName.Contains(name)) &&
                            (gradeId == 0 || s.GradeID == gradeId) && s.Activo)
                .OrderByDescending(s => s.Enrollments.Select(e => e.Course.Name).FirstOrDefault())
                .ToListAsync();

            return students;
        }

        // GET: api/Students/SearchByCourseName
        [HttpGet("SearchByCourseName")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchByCourseName(string courseName)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .Where(s => s.Enrollments.Any(e => e.Course.Name.Contains(courseName)) && s.Activo)
                .OrderBy(s => s.Enrollments.Select(e => e.Course.Name).FirstOrDefault())
                .ThenBy(s => s.LastName)
                .ToListAsync();

            return students;
        }

        // GET: api/Students/SearchByGrade
        [HttpGet("SearchByGrade")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchByGrade(int gradeId)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .Where(s => s.GradeID == gradeId && s.Activo)
                .OrderBy(s => s.Enrollments.Select(e => e.Course.Name).FirstOrDefault())
                .ThenBy(s => s.LastName)
                .ToListAsync();

            return students;
        }


    }
}