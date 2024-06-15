using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laboratorio13_Arguedas.Models;

namespace Laboratorio13_Arguedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            if (_context.Grades == null)
            {
                return NotFound();
            }
            return await _context.Grades.Where(g => g.Activo).ToListAsync();
        }

        // GET: api/Grades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            if (_context.Grades == null)
            {
                return NotFound();
            }
            var grade = await _context.Grades.FindAsync(id);

            if (grade == null || !grade.Activo)
            {
                return NotFound();
            }

            return grade;
        }

        // PUT: api/Grades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, Grade grade)
        {
            if (id != grade.GradeId)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Grades
        [HttpPost]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            if (_context.Grades == null)
            {
                return Problem("Entity set 'SchoolContext.Grades' is null.");
            }
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrade", new { id = grade.GradeId }, grade);
        }

        // DELETE: api/Grades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            if (_context.Grades == null)
            {
                return NotFound();
            }
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null || !grade.Activo)
            {
                return NotFound();
            }

            grade.Activo = false;
            _context.Entry(grade).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeExists(int id)
        {
            return (_context.Grades?.Any(e => e.GradeId == id && e.Activo)).GetValueOrDefault();
        }
    }
}
