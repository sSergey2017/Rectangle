using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rectangles.Domain;

namespace Rectangle.Persistence.RectangleTypeConfigurations
{
    public class RectangleTypeConfiguration : IEntityTypeConfiguration<CoordRectangle>
    {
        public void Configure(EntityTypeBuilder<CoordRectangle> builder)
        {
            builder.HasKey(ev => ev.Id);
            builder.HasIndex(ev => ev.Id).IsUnique();
            builder.HasIndex(ev => new { ev.LeftX, ev.RightX, ev.LeftY, ev.RightY });
        }
    }
}
