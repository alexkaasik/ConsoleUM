using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleUM.Models
{
    public class Student
    {

        public int Id { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 4)]
        public string LastName { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 4)]
        //        [Column("FirstName")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime EnrollmentDate { get; set; }

        
        public string FullName
        { 
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
