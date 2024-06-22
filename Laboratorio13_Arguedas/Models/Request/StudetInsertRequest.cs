namespace Laboratorio13_Arguedas.Models.Request
{
    public class StudetInsertRequest
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int GradeID { get; set; }
    }
}
