namespace CrossPlatformLibrary.Tracing
{
    public class EmptyTracerFactory : TracerFactoryBase
    {
        private readonly ITracer tracer;

        public EmptyTracerFactory()
        {
            this.tracer = new EmptyTracer();
        }

        public override ITracer Create(string name)
        {
            return this.tracer;
        }
    }
}