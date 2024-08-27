using ConsoleUM.Models;
using Microsoft.Identity.Client;

namespace ConsoleUM.Data
{
    public class DbInitializer
    {
        public static void Intialize(SchoolContext context) {
            context.Database.EnsureCreated();
            if (context.Student.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student {FirstName="Aleksander",LastName="Kaasik",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="Egor",LastName="Slava",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="Arsen",LastName="Arson",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="Alen",LastName="Alex",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="Know",LastName="Who",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="KnockKnock",LastName="Joke",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="Who",LastName="There",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="TheMailer",LastName="Postman",EnrollmentDate=DateTime.Parse("2004-9-1") },
                new Student {FirstName="",LastName="",EnrollmentDate=DateTime.Parse("2004-9-1") }
            };
            foreach (Student s in students)
            {
                context.Student.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseId=12,Title="Chemistry",Credits=3},
                new Course{CourseId=18,Title="Microeconomics",Credits=4},
                new Course{CourseId=1,Title="Macroeconomics",Credits=1},
                new Course{CourseId=2,Title="Calculus",Credits=12},
                new Course{CourseId=6,Title="Trigonometry",Credits=5},
                new Course{CourseId=32,Title="Composition",Credits=5},
                new Course{CourseId=11,Title="Literature",Credits=9}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();


            var enrollments = new Enrollment[] 
            {
                new Enrollment{StudentId=1,CourseId=12,Grade=Grade.B},
                new Enrollment{StudentId=1,CourseId=11,Grade=Grade.A},
                new Enrollment{StudentId=11,CourseId=12},
                new Enrollment{StudentId=12,CourseId=12,Grade=Grade.B},
                new Enrollment{StudentId=32,CourseId=12,Grade=Grade.D},
                new Enrollment{StudentId=6,CourseId=12,Grade=Grade.F},
                new Enrollment{StudentId=2,CourseId=12,Grade=Grade.B},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();


        }
    }
}
