using System;

namespace CrossPlatformLibrary.Internals
{
    public interface ITracer
    {
        void Error(string errorMessage);
        void Info(string errorMessage);
        void FatalError(Exception exception);
        void Debug(string msg);
        void Exception(Exception exception, string msg);
    }

    public class Tracer : ITracer
    {
        public static void ArgumentNotNull<T>(T param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} must not be null");
            }
        }

        public static ITracer Create<T>(T obj)
        {
            return new Tracer();
        }

        public static ITracer Create<T>()
        {
            return new Tracer();
        }

        public void Error(string errorMessage)
        {
            throw new NotImplementedException();
        }

        public void Info(string errorMessage)
        {
            throw new NotImplementedException();
        }

        public void FatalError(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(string msg)
        {
            throw new NotImplementedException();
        }

        public void Exception(Exception exception, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
