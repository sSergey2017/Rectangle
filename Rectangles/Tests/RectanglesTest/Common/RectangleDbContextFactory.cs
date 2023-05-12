using Rectangle.Persistence;
using Microsoft.EntityFrameworkCore;
using Rectangles.Domain;

namespace RectanglesTest.Common
{
    public class RectangleDbContextFactory
    {
        public static RectangleDbContext Create()
        {
            var options = new DbContextOptionsBuilder<RectangleDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new RectangleDbContext(options);
            context.Database.EnsureCreated();
            context.Rectangles.RemoveRange(context.Rectangles);
            context.SaveChangesAsync();
            context.Rectangles.AddRange(
                new CoordRectangle { Id = 1, LeftX = -50, LeftY = 50, RightX = 50, RightY = -50 },
                new CoordRectangle { Id = 2, LeftX = -40, LeftY = 60, RightX = 60, RightY = -40 },
                new CoordRectangle { Id = 3, LeftX = -30, LeftY = 70, RightX = 70, RightY = -30 },
                new CoordRectangle { Id = 4, LeftX = -20, LeftY = 80, RightX = 80, RightY = -20 },
                new CoordRectangle { Id = 5, LeftX = -10, LeftY = 90, RightX = 90, RightY = -10 },
                new CoordRectangle { Id = 6, LeftX = 0, LeftY = 100, RightX = 100, RightY = 0 },
                new CoordRectangle { Id = 7, LeftX = 10, LeftY = 110, RightX = 110, RightY = 10 },
                new CoordRectangle { Id = 8, LeftX = 20, LeftY = 120, RightX = 120, RightY = 20 },
                new CoordRectangle { Id = 9, LeftX = 30, LeftY = 130, RightX = 130, RightY = 30 },
                new CoordRectangle { Id = 10, LeftX = 40, LeftY = 140, RightX = 140, RightY = 40 }
                );

            context.SaveChanges();
            return context;
        }

        public static void Destroy(RectangleDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
