namespace CrossPlatformLibrary.Tracing
{
    internal class DefaultTracerFactoryConfiguration : IDefaultTracerFactoryConfiguration
    {
        public ITracerFactory GetDefaultTracerFactory()
        {
            return new DebugTracerFactory();
        }
    }
}