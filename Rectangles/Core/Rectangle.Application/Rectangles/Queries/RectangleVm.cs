using AutoMapper;
using Microsoft.Extensions.Logging;
using Rectangle.Application.Common.Mapping;
using Rectangles.Domain;

namespace Rectangle.Application.Rectangles.Queries
{
    public class RectangleVm : IMapWith<CoordRectangle>
    {
        public int Id { get; set; }

        // Top left corner coordinates
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }

        // Bottom right corner coordinates
        public int BottomRightX { get; set; }
        public int BottomRightY { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CoordRectangle, RectangleVm>()
                .ForMember(dest => dest.TopLeftX, opt => opt.MapFrom(src => src.LeftX))
                .ForMember(dest => dest.TopLeftY, opt => opt.MapFrom(src => src.LeftY))
                .ForMember(dest => dest.BottomRightX, opt => opt.MapFrom(src => src.RightX))
                .ForMember(dest => dest.BottomRightY, opt => opt.MapFrom(src => src.RightY));
        }
    }
}
