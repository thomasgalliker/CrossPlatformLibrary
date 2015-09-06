// ****************************************************************************
// <copyright file="SimpleIoc.cs" company="GalaSoft Laurent Bugnion">
// Copyright © GalaSoft Laurent Bugnion 2011-2015
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>10.4.2011</date>
// <project>GalaSoft.MvvmLight.Extras</project>
// <web>http://www.mvvmlight.net</web>
// <license>
// See license.txt in this project or http://www.galasoft.ch/license_MIT.txt
// </license>
// <LastBaseLevel>BL0005</LastBaseLevel>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;

using CrossPlatformLibrary.Tools.PlatformSpecific;
using CrossPlatformLibrary.Utils;

using Microsoft.Practices.ServiceLocation;

namespace CrossPlatformLibrary.IoC
{
    /// <summary>
    ///     A very simple IOC container with basic functionality needed to register and resolve
    ///     instances. If needed, this class can be replaced by another more elaborate
    ///     IOC container implementing the IServiceLocator interface.
    ///     The inspiration for this class is at https://gist.github.com/716137 but it has
    ///     been extended with additional features.
    /// </summary>
    //// [ClassInfo(typeof(SimpleIoc),
    ////  VersionString = "5.1.9",
    ////  DateString = "201502072030",
    ////  Description = "A very simple IOC container.",
    ////  UrlContacts = "http://www.galasoft.ch/contact_en.html",
    ////  Email = "laurent@galasoft.ch")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ioc")]
    public class SimpleIoc : ISimpleIoc
    {
        private static SimpleIoc instance;

        private const bool DefaultCacheUsage = true;

        private readonly Dictionary<Type, ConstructorInfo> _constructorInfos = new Dictionary<Type, ConstructorInfo>();

        private readonly string _defaultKey = Guid.NewGuid().ToString();

        private readonly Dictionary<Type, Dictionary<string, Delegate>> _factories = new Dictionary<Type, Dictionary<string, Delegate>>();

        private readonly Dictionary<Type, Dictionary<string, object>> _instancesRegistry = new Dictionary<Type, Dictionary<string, object>>();

        private readonly Dictionary<Type, Type> _interfaceToClassMap = new Dictionary<Type, Type>();

        private readonly object _syncLock = new object();

        private IAdapterResolver adapterResolver;

        /// <summary>
        ///     This class' default instance.
        /// </summary>
        public static SimpleIoc Default
        {
            get
            {
                return instance ?? (instance = new SimpleIoc());
            }
        }

        private SimpleIoc()
        {
            var defaultRegistrationConvention = new DefaultRegistrationConvention();
            this.adapterResolver = new ProbingAdapterResolver(defaultRegistrationConvention);
            
        }

        /// <summary>
        ///     Checks whether at least one instance of a given class is already created in the container.
        /// </summary>
        /// <typeparam name="TClass">The class that is queried.</typeparam>
        /// <returns>True if at least on instance of the class is already created, false otherwise.</returns>
        public bool ContainsCreated<TClass>()
        {
            return this.ContainsCreated<TClass>(null);
        }

        /// <summary>
        ///     Checks whether the instance with the given key is already created for a given class
        ///     in the container.
        /// </summary>
        /// <typeparam name="TClass">The class that is queried.</typeparam>
        /// <param name="key">The key that is queried.</param>
        /// <returns>
        ///     True if the instance with the given key is already registered for the given class,
        ///     false otherwise.
        /// </returns>
        public bool ContainsCreated<TClass>(string key)
        {
            var classType = typeof(TClass);

            if (!this._instancesRegistry.ContainsKey(classType))
            {
                return false;
            }

            if (string.IsNullOrEmpty(key))
            {
                return this._instancesRegistry[classType].Count > 0;
            }

            return this._instancesRegistry[classType].ContainsKey(key);
        }

        /// <summary>
        ///     Gets a value indicating whether a given type T is already registered.
        /// </summary>
        /// <typeparam name="T">The type that the method checks for.</typeparam>
        /// <returns>True if the type is registered, false otherwise.</returns>
        public bool IsRegistered<T>()
        {
            var classType = typeof(T);
            return this._interfaceToClassMap.ContainsKey(classType);
        }

        /// <summary>
        ///     Gets a value indicating whether a given type T is already registered.
        /// </summary>
        /// <typeparam name="T">The type that the method checks for.</typeparam>
        /// <returns>True if the type is registered, false otherwise.</returns>
        public bool IsRegistered(Type classType)
        {
            return this._interfaceToClassMap.ContainsKey(classType);
        }

        /// <summary>
        ///     Gets a value indicating whether a given type T and a give key
        ///     are already registered.
        /// </summary>
        /// <typeparam name="T">The type that the method checks for.</typeparam>
        /// <param name="key">The key that the method checks for.</param>
        /// <returns>True if the type and key are registered, false otherwise.</returns>
        public bool IsRegistered<T>(string key)
        {
            var classType = typeof(T);

            if (!this.IsRegistered(classType) || !this._factories.ContainsKey(classType))
            {
                return false;
            }

            return this._factories[classType].ContainsKey(key); // TODO GATH: Add lock (because of threading issues)
        }


        /// <summary>
        ///     Registers a given type for a given interface using the default registration convention.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        public void RegisterWithConvention<TInterface>() where TInterface : class
        {
            this.RegisterWithConvention<TInterface>(this.adapterResolver.RegistrationConvention);
        }

        /// <summary>
        ///     Registers a given type for a given interface using a specified registration convention.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <param name="convention">The convention used to convert between the given interface type and the class.</param>
        public void RegisterWithConvention<TInterface>(IRegistrationConvention convention) where TInterface : class
        {
            Guard.ArgumentNotNull(() => convention);

            this.adapterResolver.RegistrationConvention = convention;
            var classType = this.adapterResolver.ResolveClassType(typeof(TInterface));
            this.Register<TInterface>(classType);
        }

        internal void SetAdapterResolver(IAdapterResolver resolver)
        {
            Guard.ArgumentNotNull(() => resolver);

            this.adapterResolver = resolver;
        }

        /// <summary>
        ///     Registers a given type for a given interface.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <typeparam name="TClass">The type that must be used to create instances.</typeparam>
        public void Register<TInterface, TClass>()
            where TClass : class
            where TInterface : class
        {
            this.Register<TInterface, TClass>(null, false);
        }

        /// <summary>
        ///     Registers a given type for a given interface.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <param name="classType">The type that must be used to create instances.</param>
        public void Register<TInterface>(Type classType) where TInterface : class
        {
            this.Register<TInterface>(classType, createInstanceImmediately: false);
        }

        /// <summary>
        ///     Registers a given type for a given interface.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <param name="classType">The type that must be used to create instances.</param>
        public void Register<TInterface>(Type classType, bool createInstanceImmediately) where TInterface : class
        {
            this.Register<TInterface>(classType, null, createInstanceImmediately);
        }

        /// <summary>
        ///     Registers a given type for a given interface.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <typeparam name="TClass">The type that must be used to create instances.</typeparam>
        public void Register<TInterface, TClass>(bool createInstanceImmediately)
            where TInterface : class
            where TClass : class
        {
            this.Register<TInterface>(typeof(TClass), null, createInstanceImmediately);
        }

        /// <summary>
        ///     Registers a given type for a given interface.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <typeparam name="TClass">The type that must be used to create instances.</typeparam>
        public void Register<TInterface, TClass>(string key, bool createInstanceImmediately)
            where TClass : class
            where TInterface : class
        {
            this.Register<TInterface>(typeof(TClass), key, createInstanceImmediately);
        }

        /// <summary>
        ///     Registers a given type for a given interface with the possibility for immediate
        ///     creation of the instance.
        /// </summary>
        /// <typeparam name="TInterface">The interface for which instances will be resolved.</typeparam>
        /// <param name="classType">The type that must be used to create instances.</param>
        /// <param name="key">The key that the method checks for.</param>
        /// <param name="createInstanceImmediately">
        ///     If true, forces the creation of the default
        ///     instance of the provided class.
        /// </param>
        public void Register<TInterface>(Type classType, string key, bool createInstanceImmediately) where TInterface : class
        {
            lock (this._syncLock)
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = this._defaultKey;
                }

                var interfaceType = typeof(TInterface);

                if (this._interfaceToClassMap.ContainsKey(interfaceType))
                {
                    if (this._interfaceToClassMap[interfaceType] != classType)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is already a class registered for {0}.", interfaceType.FullName));
                    }
                }
                else
                {
                    this._interfaceToClassMap.Add(interfaceType, classType);
                    this._constructorInfos.Add(classType, this.GetConstructorInfo(classType));
                }

                Func<object> factory = () => this.MakeInstance(classType);
                this.DoRegister(interfaceType, factory, key);

                if (createInstanceImmediately)
                {
                    this.GetInstance<TInterface>();
                }
            }
        }

        /// <summary>
        ///     Registers a given type.
        /// </summary>
        /// <typeparam name="TClass">The type that must be used to create instances.</typeparam>
        public void Register<TClass>() where TClass : class
        {
            this.Register<TClass>(key: null, createInstanceImmediately: false);
        }

        /// <summary>
        ///     Registers a given type with the possibility for immediate
        ///     creation of the instance.
        /// </summary>
        /// <typeparam name="TClass">The type that must be used to create instances.</typeparam>
        /// <param name="createInstanceImmediately">
        ///     If true, forces the creation of the default
        ///     instance of the provided class.
        /// </param>
        public void Register<TClass>(bool createInstanceImmediately) where TClass : class
        {
            this.Register<TClass>(key: null, createInstanceImmediately: createInstanceImmediately);
        }

        public void Register<TClass>(string key, bool createInstanceImmediately) where TClass : class
        {
            var classType = typeof(TClass);
            this.Register(classType, key, createInstanceImmediately);
        }

        /// <summary>
        ///     Registers a given type with the possibility for immediate
        ///     creation of the instance.
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="key">The key for which the given instance is registered.</param>
        /// <param name="createInstanceImmediately">
        ///     If true, forces the creation of the default
        ///     instance of the provided class.
        /// </param>
        public void Register(Type classType, string key, bool createInstanceImmediately)
        {
            
#if NETFX_CORE
            if (classType.GetTypeInfo().IsInterface)
#else
            if (classType.IsInterface)
#endif
            {
                throw new ArgumentException("An interface cannot be registered alone.");
            }

            lock (this._syncLock)
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = this._defaultKey;
                }

                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(key))
                {
                    if (!this._constructorInfos.ContainsKey(classType))
                    {
                        // Throw only if constructorinfos have not been
                        // registered, which means there is a default factory
                        // for this class.
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Class {0} is already registered.", classType));
                    }

                    return;
                }

                if (!this._interfaceToClassMap.ContainsKey(classType))
                {
                    this._interfaceToClassMap.Add(classType, null);
                }

                this._constructorInfos.Add(classType, this.GetConstructorInfo(classType));
                Func<object> factory = () => this.MakeInstance(classType);
                this.DoRegister(classType, factory, key);

                if (createInstanceImmediately)
                {
                    this.GetInstance(classType);
                }
            }
        }

        /// <summary>
        ///     Registers a given instance for a given type.
        /// </summary>
        /// <typeparam name="TClass">The type that is being registered.</typeparam>
        /// <param name="factory">
        ///     The factory method able to create the instance that
        ///     must be returned when the given type is resolved.
        /// </param>
        public void Register<TClass>(Func<TClass> factory) where TClass : class
        {
            this.Register(factory, false);
        }

        public void Register<TClass>(Func<Type, TClass> factory) where TClass : class
        {
            this.Register<TClass>(factory, false);
        }

        /// <summary>
        ///     Registers a given instance for a given type with the possibility for immediate
        ///     creation of the instance.
        /// </summary>
        /// <typeparam name="TClass">The type that is being registered.</typeparam>
        /// <param name="factory">
        ///     The factory method able to create the instance that
        ///     must be returned when the given type is resolved.
        /// </param>
        /// <param name="createInstanceImmediately">
        ///     If true, forces the creation of the default
        ///     instance of the provided class.
        /// </param>
        public void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately) where TClass : class
        {
            this.Register<TClass>((Delegate)factory, createInstanceImmediately);
        }

        public void Register<TClass>(Func<Type, TClass> factory, bool createInstanceImmediately) where TClass : class
        {
            this.Register<TClass>((Delegate)factory, createInstanceImmediately);
        }

        private void Register<TClass>(Delegate factory, bool createInstanceImmediately) where TClass : class
        {
            Guard.ArgumentNotNull(() => factory);

            lock (this._syncLock)
            {
                var classType = typeof(TClass);

                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(this._defaultKey))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is already a factory registered for {0}.", classType.FullName));
                }

                if (!this._interfaceToClassMap.ContainsKey(classType))
                {
                    this._interfaceToClassMap.Add(classType, null);
                }

                this.DoRegister(classType, factory, this._defaultKey);

                if (createInstanceImmediately)
                {
                    this.GetInstance<TClass>();
                }
            }
        }

        /// <summary>
        ///     Registers a given instance for a given type and a given key.
        /// </summary>
        /// <typeparam name="TClass">The type that is being registered.</typeparam>
        /// <param name="factory">
        ///     The factory method able to create the instance that
        ///     must be returned when the given type is resolved.
        /// </param>
        /// <param name="key">The key for which the given instance is registered.</param>
        public void Register<TClass>(Func<TClass> factory, string key) where TClass : class
        {
            this.Register(factory, key, false);
        }

        /// <summary>
        ///     Registers a given instance for a given type and a given key with the possibility for immediate
        ///     creation of the instance.
        /// </summary>
        /// <typeparam name="TClass">The type that is being registered.</typeparam>
        /// <param name="factory">
        ///     The factory method able to create the instance that
        ///     must be returned when the given type is resolved.
        /// </param>
        /// <param name="key">The key for which the given instance is registered.</param>
        /// <param name="createInstanceImmediately">
        ///     If true, forces the creation of the default
        ///     instance of the provided class.
        /// </param>
        public void Register<TClass>(Func<TClass> factory, string key, bool createInstanceImmediately) where TClass : class
        {
            this.Register<TClass>((Delegate)factory, key, createInstanceImmediately);
        }

        private void Register<TClass>(Delegate factory, string key, bool createInstanceImmediately) where TClass : class
        {
            lock (this._syncLock)
            {
                var classType = typeof(TClass);

                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(key))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is already a factory registered for {0} with key {1}.", classType.FullName, key));
                }

                if (!this._interfaceToClassMap.ContainsKey(classType))
                {
                    this._interfaceToClassMap.Add(classType, null);
                }

                this.DoRegister(classType, factory, key);

                if (createInstanceImmediately)
                {
                    this.GetInstance<TClass>(key);
                }
            }
        }

        /// <summary>
        ///     Resets the instance in its original states. This deletes all the
        ///     registrations.
        /// </summary>
        public void Reset()
        {
            this._interfaceToClassMap.Clear();
            this._instancesRegistry.Clear();
            this._constructorInfos.Clear();
            this._factories.Clear();
        }

        /// <summary>
        ///     Unregisters a class from the cache and removes all the previously
        ///     created instances.
        /// </summary>
        /// <typeparam name="TClass">The class that must be removed.</typeparam>
        public void Unregister<TClass>() where TClass : class
        {
            this.Unregister(typeof(TClass));
        }

        /// <summary>
        ///     Unregisters a class from the cache and removes all the previously
        ///     created instances.
        /// </summary>
        public void Unregister(Type serviceType)
        {
            lock (this._syncLock)
            {
                Type resolveTo;

                if (this._interfaceToClassMap.ContainsKey(serviceType))
                {
                    resolveTo = this._interfaceToClassMap[serviceType] ?? serviceType;
                }
                else
                {
                    resolveTo = serviceType;
                }

                if (this._instancesRegistry.ContainsKey(serviceType))
                {
                    this._instancesRegistry.Remove(serviceType);
                }

                if (this._interfaceToClassMap.ContainsKey(serviceType))
                {
                    this._interfaceToClassMap.Remove(serviceType);
                }

                if (this._factories.ContainsKey(serviceType))
                {
                    this._factories.Remove(serviceType);
                }

                if (this._constructorInfos.ContainsKey(resolveTo))
                {
                    this._constructorInfos.Remove(resolveTo);
                }
            }
        }

        /// <summary>
        ///     Removes the given instance from the cache. The class itself remains
        ///     registered and can be used to create other instances.
        /// </summary>
        /// <typeparam name="TClass">The type of the instance to be removed.</typeparam>
        /// <param name="instance">The instance that must be removed.</param>
        public void Unregister<TClass>(TClass instance) where TClass : class
        {
            lock (this._syncLock)
            {
                var classType = typeof(TClass);

                if (this._instancesRegistry.ContainsKey(classType))
                {
                    var instances = this._instancesRegistry[classType];

                    var pairs = instances.Where(pair => pair.Value == instance).ToList();
                    for (var index = 0; index < pairs.Count(); index++)
                    {
                        instances.Remove(pairs[index].Key);
                    }
                }
            }
        }

        public void Unregister<TClass>(string key) where TClass : class
        {
            this.Unregister(typeof(TClass), key);
        }

        /// <summary>
        ///     Removes the instance corresponding to the given key from the cache. The class itself remains
        ///     registered and can be used to create other instances.
        /// </summary>
        /// <typeparam name="TClass">The type of the instance to be removed.</typeparam>
        /// <param name="classType"></param>
        /// <param name="key">The key corresponding to the instance that must be removed.</param>
        public void Unregister(Type classType, string key)
        {
            lock (this._syncLock)
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = this._defaultKey;
                }

                if (this._instancesRegistry.ContainsKey(classType))
                {
                    var instances = this._instancesRegistry[classType];

                    var pairs = instances.Where(pair => pair.Key == key).ToList();
                    for (var index = 0; index < pairs.Count(); index++)
                    {
                        instances.Remove(pairs[index].Key);
                    }
                }

                if (this._factories.ContainsKey(classType))
                {
                    if (this._factories[classType].ContainsKey(key))
                    {
                        this._factories[classType].Remove(key);
                    }
                }
            }
        }

        private object DoGetService(Type serviceType, Type parentType, string key, bool cache = DefaultCacheUsage, bool throwIfNotFound = true)
        {
            lock (this._syncLock)
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = this._defaultKey;
                }

                Dictionary<string, object> instances = null;

                if (!this._instancesRegistry.ContainsKey(serviceType))
                {
                    if (!this.IsRegistered(serviceType))
                    {
                        if (throwIfNotFound)
                        {
                            if (parentType != null)
                            {
                                throw new ActivationException(
                                    string.Format(
                                        CultureInfo.InvariantCulture,
                                        "Could not construct type {0} because one of its dependencies could not be resolved: {1}.",
                                        parentType.FullName,
                                        serviceType.FullName));
                            }
                            
                            throw new ActivationException(string.Format(CultureInfo.InvariantCulture, "Type not found in cache: {0}.", serviceType.FullName));
                        }
                        
                        return null;
                    }

                    if (cache)
                    {
                        instances = new Dictionary<string, object>();
                        this._instancesRegistry.Add(serviceType, instances);
                    }
                }
                else
                {
                    instances = this._instancesRegistry[serviceType];
                }

                if (instances != null && instances.ContainsKey(key))
                {
                    return instances[key];
                }

                object instance = null;

                if (this._factories.ContainsKey(serviceType))
                {
                    if (this._factories[serviceType].ContainsKey(key))
                    {
                        var del = this._factories[serviceType][key];
                        if (del.GetType().GenericTypeArguments.Count() == 2 && del.GetType().GenericTypeArguments[0] == typeof(Type) && del.GetType().GenericTypeArguments[1] == serviceType)
                        {
                            instance = del.DynamicInvoke(parentType);
                        }
                        else
                        {
                            instance = del.DynamicInvoke(null);
                        }
                    }
                    else
                    {
                        if (this._factories[serviceType].ContainsKey(this._defaultKey))
                        {
                            var del = this._factories[serviceType][this._defaultKey];
                            instance = del.DynamicInvoke(null);
                        }
                        else
                        {
                            throw new ActivationException(string.Format(CultureInfo.InvariantCulture, "Type not found in cache without a key: {0}", serviceType.FullName));
                        }
                    }
                }

                if (cache && instances != null)
                {
                    instances.Add(key, instance);
                }

                return instance;
            }
        }

        private void DoRegister(Type classType, Delegate factory, string key)
        {
            if (this._factories.ContainsKey(classType))
            {
                if (this._factories[classType].ContainsKey(key))
                {
                    // The class is already registered, ignore and continue.
                    return;
                }

                this._factories[classType].Add(key, factory);
            }
            else
            {
                var keyToFactoryMapping = new Dictionary<string, Delegate> { { key, factory } };
                this._factories.Add(classType, keyToFactoryMapping);
            }
        }

        private ConstructorInfo GetConstructorInfo(Type serviceType)
        {
            Type resolveTo;

            if (this._interfaceToClassMap.ContainsKey(serviceType))
            {
                resolveTo = this._interfaceToClassMap[serviceType] ?? serviceType;
            }
            else
            {
                resolveTo = serviceType;
            }

#if NETFX_CORE
            var constructorInfos = resolveTo.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic).ToArray();
#else
            var constructorInfos = interfaceType.GetConstructors();
#endif

            if (constructorInfos.Length > 1)
            {
                if (constructorInfos.Length > 2)
                {
                    return GetPreferredConstructorInfo(constructorInfos, resolveTo);
                }

                if (constructorInfos.FirstOrDefault(i => i.Name == ".cctor") == null)
                {
                    return GetPreferredConstructorInfo(constructorInfos, resolveTo);
                }

                var first = constructorInfos.FirstOrDefault(i => i.Name != ".cctor");

                if (first == null || !first.IsPublic)
                {
                    throw new ActivationException(string.Format(CultureInfo.InvariantCulture, "Cannot register: No public constructor found in {0}.", resolveTo.Name));
                }

                return first;
            }

            if (constructorInfos.Length == 0 || (constructorInfos.Length == 1 && !constructorInfos[0].IsPublic))
            {
                throw new ActivationException(string.Format(CultureInfo.InvariantCulture, "Cannot register: No public constructor found in {0}.", resolveTo.Name));
            }

            return constructorInfos[0];
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PreferredConstructor")]
        private static ConstructorInfo GetPreferredConstructorInfo(IEnumerable<ConstructorInfo> constructorInfos, Type resolveTo)
        {
            var preferredConstructorInfo = (from t in constructorInfos
#if NETFX_CORE
                                            let attribute = t.GetCustomAttribute(typeof(PreferredConstructorAttribute))
#else
                                            let attribute = Attribute.GetCustomAttribute(t, typeof(PreferredConstructorAttribute))

#endif
                                            where attribute != null
                                            select t).FirstOrDefault();

            if (preferredConstructorInfo == null)
            {
                throw new ActivationException(
                    string.Format(CultureInfo.InvariantCulture, "Cannot register: Multiple constructors found in {0} but none marked with PreferredConstructor.", resolveTo.Name));
            }

            return preferredConstructorInfo;
        }

        private static bool GetUseCacheAnnotation(Type interfaceType)
        {
            var attribute = interfaceType.GetTypeInfo().GetCustomAttribute<UseCacheAttribute>(true);
            if (attribute != null)
            {
                return attribute.UseCache;
            }

            return DefaultCacheUsage;
        }

        private object MakeInstance(Type serviceType)
        {
            var constructor = this._constructorInfos.ContainsKey(serviceType) ? this._constructorInfos[serviceType] : this.GetConstructorInfo(serviceType);

            var parameterInfos = constructor.GetParameters();

            if (parameterInfos.Length == 0)
            {
                return constructor.Invoke(new object[0]);
            }

            var parameters = new object[parameterInfos.Length];

            foreach (var parameterInfo in parameterInfos)
            {
                bool useCache = GetUseCacheAnnotation(parameterInfo.ParameterType);
                parameters[parameterInfo.Position] = this.DoGetService(parameterInfo.ParameterType, serviceType, null, useCache);
            }

            return constructor.Invoke(parameters);
        }

        /// <summary>
        ///     Provides a way to get all the created instances of a given type available in the
        ///     cache. Registering a class or a factory does not automatically
        ///     create the corresponding instance! To create an instance, either register
        ///     the class or the factory with createInstanceImmediately set to true,
        ///     or call the GetInstance method before calling GetAllCreatedInstances.
        ///     Alternatively, use the GetAllInstances method, which auto-creates default
        ///     instances for all registered classes.
        /// </summary>
        /// <param name="serviceType">
        ///     The class of which all instances
        ///     must be returned.
        /// </param>
        /// <returns>All the already created instances of the given type.</returns>
        public IEnumerable<object> GetAllCreatedInstances(Type serviceType)
        {
            if (this._instancesRegistry.ContainsKey(serviceType))
            {
                return this._instancesRegistry[serviceType].Values;
            }

            return new List<object>();
        }

        /// <summary>
        ///     Provides a way to get all the created instances of a given type available in the
        ///     cache. Registering a class or a factory does not automatically
        ///     create the corresponding instance! To create an instance, either register
        ///     the class or the factory with createInstanceImmediately set to true,
        ///     or call the GetInstance method before calling GetAllCreatedInstances.
        ///     Alternatively, use the GetAllInstances method, which auto-creates default
        ///     instances for all registered classes.
        /// </summary>
        /// <typeparam name="TService">
        ///     The class of which all instances
        ///     must be returned.
        /// </typeparam>
        /// <returns>All the already created instances of the given type.</returns>
        public IEnumerable<TService> GetAllCreatedInstances<TService>()
        {
            var serviceType = typeof(TService);
            return this.GetAllCreatedInstances(serviceType).Select(instance => (TService)instance);
        }

        #region Implementation of IServiceProvider

        /// <summary>
        ///     Gets the service object of the specified type.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type serviceType has not
        ///     been registered before calling this method.
        /// </exception>
        /// <returns>
        ///     A service object of type <paramref name="serviceType" />.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        public object GetService(Type serviceType)
        {
            Guard.ArgumentNotNull(() => serviceType);

            return this.DoGetService(serviceType, null, this._defaultKey);
        }

        #endregion

        #region Implementation of IServiceLocator

        /// <summary>
        ///     Provides a way to get all the created instances of a given type available in the
        ///     cache. Calling this method auto-creates default
        ///     instances for all registered classes.
        /// </summary>
        /// <param name="serviceType">
        ///     The class of which all instances
        ///     must be returned.
        /// </param>
        /// <returns>All the instances of the given type.</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            Guard.ArgumentNotNull(() => serviceType);

            lock (this._factories)
            {
                if (this._factories.ContainsKey(serviceType))
                {
                    foreach (var factory in this._factories[serviceType])
                    {
                        this.GetInstance(serviceType, factory.Key);
                    }
                }
            }

            if (this._instancesRegistry.ContainsKey(serviceType))
            {
                return this._instancesRegistry[serviceType].Values;
            }

            return new List<object>();
        }

        /// <summary>
        ///     Provides a way to get all the created instances of a given type available in the
        ///     cache. Calling this method auto-creates default
        ///     instances for all registered classes.
        /// </summary>
        /// <typeparam name="TService">
        ///     The class of which all instances
        ///     must be returned.
        /// </typeparam>
        /// <returns>All the instances of the given type.</returns>
        public IEnumerable<TService> GetAllInstances<TService>()
        {
            var serviceType = typeof(TService);
            return this.GetAllInstances(serviceType).Select(instance => (TService)instance);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type. If no instance had been instantiated
        ///     before, a new instance will be created. If an instance had already
        ///     been created, that same instance will be returned.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type serviceType has not
        ///     been registered before calling this method.
        /// </exception>
        /// <param name="serviceType">
        ///     The class of which an instance
        ///     must be returned.
        /// </param>
        /// <returns>An instance of the given type.</returns>
        public object GetInstance(Type serviceType)
        {
            Guard.ArgumentNotNull(() => serviceType);

            return this.DoGetService(serviceType, null, this._defaultKey);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type. This method
        ///     always returns a new instance and doesn't cache it in the IOC container.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type serviceType has not
        ///     been registered before calling this method.
        /// </exception>
        /// <param name="serviceType">
        ///     The class of which an instance
        ///     must be returned.
        /// </param>
        /// <returns>An instance of the given type.</returns>
        public object GetInstanceWithoutCaching(Type serviceType)
        {
            Guard.ArgumentNotNull(() => serviceType);

            return this.DoGetService(serviceType, null, this._defaultKey, false);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type corresponding
        ///     to a given key. If no instance had been instantiated with this
        ///     key before, a new instance will be created. If an instance had already
        ///     been created with the same key, that same instance will be returned.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type serviceType has not
        ///     been registered before calling this method.
        /// </exception>
        /// <param name="serviceType">The class of which an instance must be returned.</param>
        /// <param name="key">The key uniquely identifying this instance.</param>
        /// <returns>An instance corresponding to the given type and key.</returns>
        public object GetInstance(Type serviceType, string key)
        {
            Guard.ArgumentNotNull(() => serviceType);

            return this.DoGetService(serviceType, null, key);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type. This method
        ///     always returns a new instance and doesn't cache it in the IOC container.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type serviceType has not
        ///     been registered before calling this method.
        /// </exception>
        /// <param name="serviceType">The class of which an instance must be returned.</param>
        /// <param name="key">The key uniquely identifying this instance.</param>
        /// <returns>An instance corresponding to the given type and key.</returns>
        public object GetInstanceWithoutCaching(Type serviceType, string key)
        {
            Guard.ArgumentNotNull(() => serviceType);

            return this.DoGetService(serviceType, null, key, false);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type. If no instance had been instantiated
        ///     before, a new instance will be created. If an instance had already
        ///     been created, that same instance will be returned.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type TService has not
        ///     been registered before calling this method.
        /// </exception>
        /// <typeparam name="TService">
        ///     The class of which an instance
        ///     must be returned.
        /// </typeparam>
        /// <returns>An instance of the given type.</returns>
        public TService GetInstance<TService>()
        {
            return (TService)this.DoGetService(typeof(TService), null, this._defaultKey);
        }

        public TService TryGetInstance<TService>()
        {
            return (TService)this.DoGetService(
                serviceType: typeof(TService),
                parentType: null, 
                key: this._defaultKey,
                cache: DefaultCacheUsage, 
                throwIfNotFound: false);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type. This method
        ///     always returns a new instance and doesn't cache it in the IOC container.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type TService has not
        ///     been registered before calling this method.
        /// </exception>
        /// <typeparam name="TService">
        ///     The class of which an instance
        ///     must be returned.
        /// </typeparam>
        /// <returns>An instance of the given type.</returns>
        public TService GetInstanceWithoutCaching<TService>()
        {
            return (TService)this.DoGetService(typeof(TService), null, this._defaultKey, false);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type corresponding
        ///     to a given key. If no instance had been instantiated with this
        ///     key before, a new instance will be created. If an instance had already
        ///     been created with the same key, that same instance will be returned.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type TService has not
        ///     been registered before calling this method.
        /// </exception>
        /// <typeparam name="TService">The class of which an instance must be returned.</typeparam>
        /// <param name="key">The key uniquely identifying this instance.</param>
        /// <returns>An instance corresponding to the given type and key.</returns>
        public TService GetInstance<TService>(string key)
        {
            return (TService)this.DoGetService(typeof(TService), null, key);
        }

        /// <summary>
        ///     Provides a way to get an instance of a given type. This method
        ///     always returns a new instance and doesn't cache it in the IOC container.
        /// </summary>
        /// <exception cref="ActivationException">
        ///     If the type TService has not
        ///     been registered before calling this method.
        /// </exception>
        /// <typeparam name="TService">The class of which an instance must be returned.</typeparam>
        /// <param name="key">The key uniquely identifying this instance.</param>
        /// <returns>An instance corresponding to the given type and key.</returns>
        public TService GetInstanceWithoutCaching<TService>(string key)
        {
            return (TService)this.DoGetService(typeof(TService), null, key, false);
        }

        #endregion
    }
}