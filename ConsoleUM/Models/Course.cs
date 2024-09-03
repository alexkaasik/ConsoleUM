
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleUM.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }


        public Department Department { get; set; }


        public ICollection<Enrollment> Enrollment { get; set; }

        public ICollection
    }
}