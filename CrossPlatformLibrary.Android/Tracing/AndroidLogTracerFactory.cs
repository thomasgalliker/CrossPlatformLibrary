using System;

namespace CrossPlatformLibrary.Tracing
{
    public class AndroidLogTracerFactory : TracerFactoryBase
    {
        public override ITracer Create(string name)
        {
            return new AndroidLogTracer(name);
        }
    }
}