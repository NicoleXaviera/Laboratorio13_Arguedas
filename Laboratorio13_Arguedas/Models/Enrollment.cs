namespace Laboratorio13_Arguedas.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public DateTime Date { get; set; }
        public int StudentID { get; set; }
        public Student? Student { get; set; }
        public int CourseID { get; set; }
        public Course? Course { get; set; }
    }
}