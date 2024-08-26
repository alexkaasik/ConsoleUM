using Microsoft.EntityFrameworkCore;

namespace ConsoleUM.Models
{
    public class Student
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
