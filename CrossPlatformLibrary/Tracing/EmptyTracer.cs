
namespace CrossPlatformLibrary.Tracing
{
    /// <summary>
    /// EmptyTracer is - as the name implies - a tracer instance which ignores all trace writes.
    /// </summary>
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