using System;

namespace CrossPlatformLibrary.Internals
{
    public interface ITracer
    {
        void Error(string message);
        void Info(string message);
        void FatalError(Exception exception);
        void Debug(string message);
        void Exception(Exception exception, string message);
    }

    public class Tracer : ITracer
    {
        public static ITracer Create<T>(T obj)
        {
            return new Tracer();
        }

        public static ITracer Create<T>()
        {
            return new Tracer();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void FatalError(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Exception(Exception exception, string message)
        {
            throw new NotImplementedException();
        }
    }
}
