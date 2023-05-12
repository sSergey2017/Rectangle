using AutoMapper;
using Rectangle.Application.Common.Mapping;
using Rectangle.Application.Rectangles.Queries;

namespace Rectangle.WebApi.Models
{
    public class PointsList : IMapWith<GetRectanglesSearchQuery>
    {
        public List<PointDto> Points { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PointsList, GetRectanglesSearchQuery>()
                .ForMember(d => d.Points, opt => opt.MapFrom(s => s.Points
                    .Select(p => new PointForQuery { X = p.X, Y = p.Y })
                    .ToList()));
        }
    }

    public class PointDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
