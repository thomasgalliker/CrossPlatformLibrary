# CrossPlatformLibrary

CrossPlatformLibrary is an extensible toolkit which addresses cross-cutting concern. It is a lightweight library which provides a collection of functionality used in most mobile and desktop applications such as bootstrapping, exception handling, tracing and UI dispatching.

#### Supported Platforms

<table>
  <tr>
    <td></td>
    <td>Android</td>
    <td>iOS
(Classic API)</td>
    <td>iOS
(Unified API)</td>
    <td>Windows Phone 8 (SL)</td>
    <td>Windows Phone 8.1</td>
    <td>Windows Store / Universal</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
</table>


### Download and Install CrossPlatformLibrary

This library is available on NuGet:[ ](https://www.nuget.org/packages/TypeConverter/)[https://www.nuget.org/packages/CrossPlatformLibrary](https://www.nuget.org/packages/CrossPlatformLibrary). Use the following command to install CrossPlatformLibrary using NuGet package manager console:

```PM> Install-Package CrossPlatformLibrary```

You can use this library in any .Net project which is compatible to PCL (e.g. Xamarin Android, iOS, Windows Phone, Windows Store, Universal Apps, etc.). Make sure you install this NuGet package to all relevant projects. NuGet automatically adds a reference to assembly CrossPlatformLibrary.dll and CrossPlatformLibrary.Platform.dll. For PCL projects it only adds the platform-independant assembly CrossPlatformLibrary.dll.

### API Usage

#### Bootstrapping

The bootstrapping mechanism is used to startup and shutdown an application in a controlled way. The boostrapper is called at the entry point of an application and it cares about the basic initialisation tasks. The entry point of the application can differ from application type to application type. Following table gives an overview from where the bootstrapper needs to be launched on different platforms:

<table>
  <tr>
    <td>Platform</td>
    <td>From where to call Boostrapper.Startup method</td>
  </tr>
  <tr>
    <td>Android</td>
    <td></td>
  </tr>
  <tr>
    <td>iOS
(Classic API)</td>
    <td></td>
  </tr>
  <tr>
    <td>iOS
(Unified API)</td>
    <td></td>
  </tr>
  <tr>
    <td>Windows Phone 8 (SL)</td>
    <td>App.cs constructor</td>
  </tr>
  <tr>
    <td>Windows Phone 8.1</td>
    <td></td>
  </tr>
  <tr>
    <td>Windows Store / Universal</td>
    <td></td>
  </tr>
</table>


CrossPlatformLibrary provides you with a base implementation of a bootstrapper. You may want to inherit the Bootstrapper class in order to influence the startup procedure and/or bootstrap parts of your own application. Following things can be done with a custom Bootstrapper:

* Register project-dependant dependencies in the IoC container

* Define your own IExceptionHandler to be used for unhandled exceptions

* Handle exceptions that happen during Bootstrapping (BootstrappingException)

to be documented --- What is the Bootstrapping Mechanism

#### Exception Handling

Unhandled exceptions may occur in every application. Better we are prepared for such exceptions. IExceptionHandler instance is used to handle any System.Exception that is not handled by the application.

#### Tracing

The tracing functionality provided by CrossPlatformLibrary can be used to write application debug traces. The purpose of trace messages is to assist developers and 3rd level support locating and fixing bugs or other program flow related problems.

<document abstraction, design desicion>

<table>
  <tr>
    <td>Tracer Name</td>
    <td>Contained in</td>
    <td>Description</td>
  </tr>
  <tr>
    <td>EmptyTracer</td>
    <td>CrossPlatformLibrary</td>
    <td>EmptyTracer doesn’t trace anything. Each call to the Write method will not execute anything.</td>
  </tr>
  <tr>
    <td>DebugTracer</td>
    <td>CrossPlatformLibrary</td>
    <td>DebugTracer uses System.Diagnostics.Debug.WriteLine to write traces.</td>
  </tr>
  <tr>
    <td>ActionTracer</td>
    <td>CrossPlatformLibrary</td>
    <td>ActionTracer allows you to define an Action/Delegate to which traces are written to.</td>
  </tr>
  <tr>
    <td>Log4NetTracer</td>
    <td><reference nuget></td>
    <td></td>
  </tr>
  <tr>
    <td>MetroLogTracer</td>
    <td><reference nuget></td>
    <td></td>
  </tr>
  <tr>
    <td></td>
    <td></td>
    <td></td>
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
    <td></td>
  </tr>
  <tr>
    <td>Information</td>
    <td></td>
  </tr>
  <tr>
    <td>Warning</td>
    <td></td>
  </tr>
  <tr>
    <td>Error</td>
    <td></td>
  </tr>
  <tr>
    <td>Fatal</td>
    <td></td>
  </tr>
</table>


#### Modularity

CrossPlatformLibrary was designed in a way which promotes modular design. Modules are loosely coupled and self-contained units which serve a concrete purpose. This approach supports the separation of concerns in your application. You can easily test modules in isolation and integrate them later into your application(s) without having them too tighly coupled. 

Following matrix shows the available modules and its supported platforms:

<table>
  <tr>
    <td></td>
    <td>Android</td>
    <td>iOS
(Classic API)</td>
    <td>iOS
(Unified API)</td>
    <td>Windows Phone 8 (SL)</td>
    <td>Windows Phone 8.1</td>
    <td>Windows Store / Universal</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Camera</td>
    <td>yes</td>
    <td>not yet</td>
    <td>not yet</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Connectivity</td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
    <td></td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Device</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Geolocation</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Market</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Maps</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Messaging</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
  <tr>
    <td>CrossPlatformLibrary.Settings</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
    <td>yes</td>
  </tr>
</table>


to be documented

#### UI Dispatching

to be documented

#### Dependency Management

Dependency injection containers are used to manage dependencies between components. This typically involes registration and instanciation and resolution of dependencies. CrossPlatformLibrary uses its own implementation of a dependency injection container, CrossPlatformLibrary.Ioc.SimpleIoc.

to be documented

### Planned features

* Encapsulate dependency injection containers so that 3rd party framworks such as AutoFac, Unity, etc.. can be used with CrossPlatformLibrary.

* Sample projects to demonstrate features.

### License

CrossPlatformLibrary is Copyright &copy; 2015 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.

