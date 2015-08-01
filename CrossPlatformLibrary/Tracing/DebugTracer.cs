using System;
using System.Diagnostics;

namespace CrossPlatformLibrary.Tracing
{
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
