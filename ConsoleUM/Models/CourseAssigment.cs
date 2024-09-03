namespace ConsoleUM.Models
{
    public class CourseAssigment
    {
        public int InstructionId { get; set; }

        public int CourseId { get; set; }

        public Instructor Instructor { get; set; }

        public Course Course { get; set; }
    }
}
