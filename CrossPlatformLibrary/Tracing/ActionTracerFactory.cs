using System;

namespace CrossPlatformLibrary.Tracing
{
    public class ActionTracerFactory : TracerFactoryBase
    {
        private readonly Action<string, TraceEntry> forwardingAction;

        public ActionTracerFactory(Action<string, TraceEntry> forwardingAction)
        {
            this.forwardingAction = forwardingAction;
        }

        public override ITracer Create(string name)
        {
            return new ActionTracer(name, this.forwardingAction);
        }
    }
}