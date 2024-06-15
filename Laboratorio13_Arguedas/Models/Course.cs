namespace Laboratorio13_Arguedas.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public bool Activo { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }


    }
}
