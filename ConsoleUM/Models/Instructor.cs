using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleUM.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [Column("FirstName")]
        [Display(Name = "Last name")]
        [StringLength(23)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]

        public DateTime HireName { get; set; }
        [Display(Name = "Full name")]
        public string FullName
        {
            get {
                return LastName + ", " + FirstName;
            }
        }

        public ICollection<CourseAssignment> CourseAssignments {}
    }
}

// Add-Migration init