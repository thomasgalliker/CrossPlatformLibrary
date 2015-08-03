using System;

namespace CrossPlatformLibrary.Tracing
{
    public class ActionTracerFactory : TracerFactoryBase
    {
        private readonly ITracer tracer;

        public ActionTracerFactory(Action<TraceEntry> forwardingAction)
        {
            this.tracer = new ActionTracer(forwardingAction);
        }

        public override ITracer Create(string name)
        {
            return this.tracer;
        }
    }
}