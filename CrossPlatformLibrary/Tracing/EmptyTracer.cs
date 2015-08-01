
namespace CrossPlatformLibrary.Tracing
{
    public class EmptyTracer : TracerBase
    {
        protected override void WriteCore(TraceEntry entry)
        {
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return false;
        }
    }
}