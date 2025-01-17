﻿namespace Laboratorio13_Arguedas.Models
{
    public class Student
    {
        public int StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public int GradeID { get; set; }
        public Boolean Activo { get; set; }
        public Grade? Grade { get; set; }
    }
}