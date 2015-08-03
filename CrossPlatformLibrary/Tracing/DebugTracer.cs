using System;
using System.Diagnostics;

namespace CrossPlatformLibrary.Tracing
{
    /// <summary>
    /// DebugTracer is a tracer instance which writes trace entries to the
    /// trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners"/> collection.
    /// </summary>
    public class DebugTracer : TracerBase
    {
        protected override void WriteCore(TraceEntry entry)
        {
            Debug.WriteLine("{0} - {1}{2}{3}", entry.Category, entry.Message, Environment.NewLine, entry.Exception);
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }
    }
}
