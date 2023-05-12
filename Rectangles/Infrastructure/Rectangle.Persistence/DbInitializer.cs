using Microsoft.EntityFrameworkCore;

namespace Rectangle.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(RectangleDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
