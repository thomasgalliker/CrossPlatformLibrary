namespace CrossPlatformLibrary.Tracing
{
    public class DebugTracerFactory : TracerFactoryBase
    {
        private readonly ITracer tracer;

        public DebugTracerFactory()
        {
            this.tracer = new DebugTracer();
        }

        public override ITracer Create(string name)
        {
            return this.tracer;
        }
    }
}