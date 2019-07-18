# CrossPlatformLibrary

CrossPlatformLibrary is an extensible toolkit which addresses cross-cutting concern. It is a lightweight library which provides a collection of functionality used in most mobile and desktop applications such as bootstrapping, exception handling, tracing and UI dispatching.

#### Supported Platforms

<table>
  <tr>
    <td></td>
    <td>.Net 4.5 / WPF</td>
    <td>.Net Standard 2.0</td>
    <td>Xamarin.Android</td>
    <td>Xamarin.iOS</td>
    <td>UWP</td>

  </tr>
  <tr>
    <td>CrossPlatformLibrary</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Forms</td>
    <td>no</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>not yet</td>
  </tr>
</table>


### Download and Install CrossPlatformLibrary

This library is available on [NuGet](https://www.nuget.org/packages/CrossPlatformLibrary). Use the following command to install CrossPlatformLibrary using NuGet package manager console:

```PM> Install-Package CrossPlatformLibrary```

The Xamarin.Forms specific library can be installed using following command:

```PM> Install-Package CrossPlatformLibrary.Forms```

### API Usage

#### Bootstrapping

The bootstrapping mechanism is used to startup and shutdown an application in a controlled way. The boostrapper is called at the entry point of an application and it cares about the basic initialisation tasks. The entry point of the application can differ from application type to application type.


CrossPlatformLibrary provides you with a base implementation of a bootstrapper. You may want to inherit the Bootstrapper class in order to influence the startup procedure and/or bootstrap parts of your own application. Following things can be done with a custom Bootstrapper:

* Register project-dependant dependencies in the IoC container

* Define your own IExceptionHandler to be used for unhandled exceptions

* Handle exceptions that happen during Bootstrapping (BootstrappingException)

```TODO: to be documented```

#### Exception Handling

Unhandled exceptions may occur in every application. Better we are prepared for such exceptions. IExceptionHandler instance is used to handle any System.Exception that is not handled by the application.

#### Tracing

The tracing functionality provided by CrossPlatformLibrary can be used to write application debug traces. The purpose of trace messages is to assist developers and 3rd level support locating and fixing bugs or other program flow related problems.

<document abstraction, design desicion>

<table>
  <tr>
    <td>Tracer Name</td>
    <td>Description</td>
  </tr>
  <tr>
    <td>EmptyTracer</td>
    <td>EmptyTracer doesn’t trace anything. Each call to the Write method will not execute anything.</td>
  </tr>
  <tr>
    <td>DebugTracer</td>
    <td>DebugTracer uses System.Diagnostics.Debug.WriteLine to write traces.</td>
  </tr>
  <tr>
    <td>ActionTracer</td>
    <td>ActionTracer allows you to define an Action/Delegate to which traces are written to.</td>
  </tr>
  <tr>
    <td>Your own ITracer implementation</td>
    <td>Write your own tracer by implementing ITracer or abstract TracerBase.</td>
  </tr>
</table>

You can implement your own tracer by either implementing the ITracer and ITracerFactory interface on you own or extend the provided base implementation of the TracerBase and TracerFactoryBase.

<table>
  <tr>
    <td>Trace Category</td>
    <td>Description</td>
  </tr>
  <tr>
    <td>Debug</td>
    <td>Information traced for debugging purposes. Usually only active in test or pre-production releases.</td>
  </tr>
  <tr>
    <td>Information</td>
    <td>Important trace information which is usually also traced by production software.</td>
  </tr>
  <tr>
    <td>Warning</td>
    <td>Some warnings which are not critical but need care.</td>
  </tr>
  <tr>
    <td>Error</td>
    <td>Typically handled exceptions which have to be traced in order to be analyzed and avoided.</td>
  </tr>
  <tr>
    <td>Fatal</td>
    <td>Software crashes, unhandled exceptions.</td>
  </tr>
</table>


#### Modularity

CrossPlatformLibrary was designed in a way which promotes modular design. Modules are loosely coupled and self-contained units which serve a concrete purpose. This approach supports the separation of concerns in your application. You can easily test modules in isolation and integrate them later into your application(s) without having them too tighly coupled. 

```TODO: to be documented```

#### UI Dispatching

```TODO: to be documented```

#### Dependency Management

Dependency injection containers are used to manage dependencies between components. This typically involes registration and instanciation and resolution of dependencies. CrossPlatformLibrary uses its own implementation of a dependency injection container, CrossPlatformLibrary.Ioc.SimpleIoc.

```TODO: to be documented```

### Planned features

* Encapsulate dependency injection containers so that 3rd party framworks such as AutoFac, Unity, etc.. can be used with CrossPlatformLibrary.

* Sample projects to demonstrate features.

### License

CrossPlatformLibrary is Copyright &copy; 2019 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.

