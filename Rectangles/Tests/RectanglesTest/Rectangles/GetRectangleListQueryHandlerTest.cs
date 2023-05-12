using AutoMapper;
using Rectangle.Application.Rectangles.Queries;
using Rectangle.Persistence;
using RectanglesTest.Common;
using Shouldly;

namespace RectanglesTest.Rectangles
{
    [Collection("QueryCollection")]
    public class GetRectangleListQueryHandlerTest
    {
        private readonly RectangleDbContext Context;
        private readonly IMapper Mapper;

        public GetRectangleListQueryHandlerTest(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetRectangleListQueryHandler(Context, Mapper);
            var points = new List<PointForQuery>
            {
                new PointForQuery { X = 0, Y = 0 },
                new PointForQuery { X = 50, Y = 50 },
                new PointForQuery { X = 80, Y = 80 },
                new PointForQuery { X = 200, Y = 200 }
            };

            // Act
            var result = await handler.Handle(
                new GetRectanglesSearchQuery
                {
                    Points = points
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<RectangleListVm>>();

        }

        [Theory]
        [InlineData(200, 200, 0)]
        [InlineData(80, 80, 7)]
        [InlineData(50, 50, 10)]
        [InlineData(0, 0, 6)]
        public async Task GetRectangleListQueryHandler_Data(int x, int y, int expectedCount)
        {
            // Arrange
            var handler = new GetRectangleListQueryHandler(Context, Mapper);
            var points = new List<PointForQuery>
            {
                new PointForQuery { X = 0, Y = 0 },
                new PointForQuery { X = 50, Y = 50 },
                new PointForQuery { X = 80, Y = 80 },
                new PointForQuery { X = 200, Y = 200 }
            };

            // Act
            var result = await handler.Handle(
                new GetRectanglesSearchQuery
                {
                    Points = points
                },
                CancellationToken.None);

            // Assert
            AssertRectangleCount(result, x, y, expectedCount);
        }

        private void AssertRectangleCount(List<RectangleListVm> list, int x, int y, int expectedCount)
        {
            var rectangles = list
                .Where(d => d.Point.X == x && d.Point.Y == y)
                .Select(r => r.Rectangles)
                .FirstOrDefault() ?? new List<RectangleVm>();

            Assert.Equal(expectedCount, rectangles.Count());
        }

    }
}
