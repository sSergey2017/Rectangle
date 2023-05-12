using Rectangle.Persistence;

namespace RectanglesTest.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly RectangleDbContext Context;

        public TestCommandBase()
        {
            Context = RectangleDbContextFactory.Create();
        }

        public void Dispose()
        {
            RectangleDbContextFactory.Destroy(Context);
        }
    }
}
