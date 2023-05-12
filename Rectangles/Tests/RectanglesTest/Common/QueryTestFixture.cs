using AutoMapper;
using Rectangle.Application.Common.Mapping;
using Rectangle.Persistence;
using Rectangle.Application.Interfaces;

namespace RectanglesTest.Common
{
    public class QueryTestFixture : IDisposable
    {
        public RectangleDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = RectangleDbContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IRectangleDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            RectangleDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
