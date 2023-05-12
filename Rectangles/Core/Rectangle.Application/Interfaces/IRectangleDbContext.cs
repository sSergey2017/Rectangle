using Microsoft.EntityFrameworkCore;
using Rectangles.Domain;

namespace Rectangle.Application.Interfaces
{
    public interface IRectangleDbContext
    {
        public DbSet<CoordRectangle> Rectangles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
