using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleUM.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100), MinimumLength = 2]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(DataType.Date)]
        

    }
}
