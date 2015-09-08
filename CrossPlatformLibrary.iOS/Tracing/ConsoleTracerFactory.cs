
namespace CrossPlatformLibrary.Tracing
{
    public class ConsoleTracerFactory : TracerFactoryBase
    {
        public override ITracer Create(string name)
        {
            return new ConsoleTracer(name);
        }
    }
}