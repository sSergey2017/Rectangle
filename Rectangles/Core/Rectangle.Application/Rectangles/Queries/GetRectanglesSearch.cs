using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rectangle.Application.Interfaces;
using Serilog;

namespace Rectangle.Application.Rectangles.Queries
{
    public class RectangleListVm
    {
        public PointForQuery Point { get; set; }
        public IList<RectangleVm> Rectangles { get; set; }
    }
    public class GetRectanglesSearchQuery : IRequest<List<RectangleListVm>>
    {
        public List<PointForQuery> Points { get; set; }
    }

    public class PointForQuery
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class GetRectangleListQueryHandler : IRequestHandler<GetRectanglesSearchQuery, List<RectangleListVm>>
    {
        private readonly IRectangleDbContext _context;
        private readonly IMapper _mapper;

        public GetRectangleListQueryHandler(IRectangleDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);


        public async Task<List<RectangleListVm>> Handle(GetRectanglesSearchQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Request: {@Request}", request);
            var results = new List<RectangleListVm>();

            foreach (var point in request.Points)
            {
                var rectanglesContainingPoint = await _context.Rectangles
                    .Where(r => r.LeftX <= point.X && r.RightX >= point.X && r.LeftY >= point.Y && r.RightY <= point.Y)
                    .ToListAsync(cancellationToken);

                var rectangleListVm = new RectangleListVm
                {
                    Point = point,
                    Rectangles = _mapper.Map<IList<RectangleVm>>(rectanglesContainingPoint)
                };

                results.Add(rectangleListVm);
            }

            return results;
        }
    }
}
