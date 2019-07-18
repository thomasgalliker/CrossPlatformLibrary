using System;

namespace CrossPlatformLibrary.IoC
{
    public interface IIocContainer : IServiceLocator
    {
        void Register<TClass>()
            where TClass : class;

        void Register<TInterface, TClass>()
            where TClass : class
            where TInterface : class;

        void Register<TInterface>(TInterface instance) 
            where TInterface : class;

        void Register<TInterface>(Type interfaceType) 
            where TInterface : class;

        void Register<TClass>(Func<TClass> factory)
            where TClass : class;

        void RegisterSingleton<TClass>()
            where TClass : class;

        void RegisterSingleton<TInterface, TClass>()
            where TClass : class
            where TInterface : class;

        void RegisterSingleton<TInterface>(TInterface instance)
            where TInterface : class;

        void RegisterSingleton<TInterface>(Type interfaceType)
            where TInterface : class;

        void RegisterSingleton<TClass>(Func<TClass> factory)
            where TClass : class;

        void Update();

        void Reset();
    }
}