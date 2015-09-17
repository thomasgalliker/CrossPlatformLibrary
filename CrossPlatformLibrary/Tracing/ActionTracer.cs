using System;

using Guards;

namespace CrossPlatformLibrary.Tracing
{
    /// <summary>
    /// ActionTracer can be used to intercept trace writes.
    /// The defined forwardingAction is called whenever a trace is written to the ITracer interface.
    /// </summary>
    public class ActionTracer : TracerBase
    {
        private readonly Action<string, TraceEntry> forwardingAction;
        private readonly string name;

        public ActionTracer(string name, Action<string, TraceEntry> forwardingAction)
        {
            Guard.ArgumentNotNullOrEmpty(() => name);
            Guard.ArgumentNotNull(() => forwardingAction);

            this.name = name;
            this.forwardingAction = forwardingAction;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            this.forwardingAction(this.name, entry);
        }

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }
    }
}
