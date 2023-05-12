
namespace Rectangles.Domain
{
    public class CoordRectangle
    {
        public int Id { get; set; }

        // Top left corner coordinates
        public int LeftX { get; set; }
        public int LeftY { get; set; }

        //Bottom right corner of rectangle
        public int RightX { get; set; }
        public int RightY { get; set; }

    }
}
