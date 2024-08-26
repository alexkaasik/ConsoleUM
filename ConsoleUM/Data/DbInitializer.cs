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
        }
    }
}
