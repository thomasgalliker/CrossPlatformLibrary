namespace CrossPlatformLibrary.Tracing
{
    internal interface IDefaultTracerFactoryConfiguration
    {
        ITracerFactory GetDefaultTracerFactory();
    }
}