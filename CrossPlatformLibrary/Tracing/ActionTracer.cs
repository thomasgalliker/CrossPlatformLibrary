using System;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tracing
{
    /// <summary>
    /// ActionTracer can be used to intercept trace writes.
    /// The defined forwardingAction is called whenever a trace is written to the ITracer interface.
    /// </summary>
    public class ActionTracer : TracerBase
    {
        private readonly Action<TraceEntry> forwardingAction;

        public ActionTracer(Action<TraceEntry> forwardingAction)
        {
            Guard.ArgumentNotNull(() => forwardingAction);

            this.forwardingAction = forwardingAction;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            this.forwardingAction(entry);
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }
    }
}
