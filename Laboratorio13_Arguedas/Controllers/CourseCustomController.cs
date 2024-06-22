using Laboratorio13_Arguedas.Models;
using Laboratorio13_Arguedas.Models.Request;
using Microsoft.AspNetCore.Mvc;


namespace Laboratorio13_Arguedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCustomController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CourseCustomController(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Insert(CourseInsertRequest request)
        {
            //Request=>Model
            Course course = new Course();
            course.Name = request.Name;
            course.Credit = request.Credit;
            course.Activo = true;
    //   course.Courses.Add(request);
      //     context.SaveChanges;

        }

        [HttpDelete]
        public void Delete(CourseDeleteRequest request)
        {

        }

    }
}
