using Microsoft.EntityFrameworkCore;
using Rectangle.Application.Interfaces;
using Rectangle.Persistence.RectangleTypeConfigurations;
using Rectangles.Domain;

namespace Rectangle.Persistence
{
    public class RectangleDbContext : DbContext, IRectangleDbContext
    {
        public DbSet<CoordRectangle> Rectangles { get; set; }

        public RectangleDbContext(DbContextOptions<RectangleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            List<CoordRectangle> rectangles = GenerateDataRectangles();
            builder.ApplyConfiguration(new RectangleTypeConfiguration());
            builder.Entity<CoordRectangle>().HasData(rectangles);
            base.OnModelCreating(builder);
        }

        private List<CoordRectangle> GenerateDataRectangles()
        {
            var random = new Random();
            var rectangles = new List<CoordRectangle>();

            for (var i = 1; i <= 200; i++)
            {
                int leftX = random.Next(-100, 100);
                int leftY = random.Next(-100, 100);

                int rightX = leftX + random.Next(1, 100);
                int rightY = leftY - random.Next(1, 100);

                var rectangle = new CoordRectangle
                {
                    Id = i,
                    LeftX = leftX,
                    LeftY = leftY,
                    RightX = rightX,
                    RightY = rightY
                };

                rectangles.Add(rectangle);
            }

            return rectangles;
        }
    }
}
