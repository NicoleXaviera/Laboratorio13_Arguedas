using Laboratorio13_Arguedas.Models;
using Laboratorio13_Arguedas.Models.Request;
using Laboratorio13_Arguedas.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio13_Arguedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCustomController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _configuration;

        public CourseCustomController(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var validationResult = ValidationHelper.GetRole(user.UserName, user.Password);

            if (validationResult.IsValid)
            {
                var token = GenerateJwtToken(user.UserName, validationResult.Role);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert(CourseInsertRequest request)
        {
            Course course = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                Activo = true
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Insert), new { id = course.CourseID }, course);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(CourseDeleteRequest request)
        {
            var course = await _context.Courses.FindAsync(request.CourseID);
            if (course == null)
            {
                return NotFound();
            }
            course.Activo = false;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Puedes agregar más métodos aquí según sea necesario, por ejemplo:

        [HttpGet]
        [Authorize] // Cualquier usuario autenticado puede acceder
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses.Where(c => c.Activo).ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [Authorize] // Cualquier usuario autenticado puede acceder
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null || !course.Activo)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, CourseUpdateRequest request)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null || !course.Activo)
            {
                return NotFound();
            }

            course.Name = request.Name;
            course.Credit = request.Credit;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}