# CrossPlatformLibrary
[![Version](https://img.shields.io/nuget/v/CrossPlatformLibrary.svg)](https://www.nuget.org/packages/CrossPlatformLibrary)  [![Downloads](https://img.shields.io/nuget/dt/CrossPlatformLibrary.svg)](https://www.nuget.org/packages/CrossPlatformLibrary)

<img src="https://raw.githubusercontent.com/thomasgalliker/CrossPlatformLibrary/master/Images/cpl_short.png" alt="CrossPlatformLibrary" align="right" height="100">
CrossPlatformLibrary is an extensible toolkit which addresses cross-cutting concern. It is a lightweight library which provides a collection of functionality used in most mobile and desktop applications such as bootstrapping, exception handling, tracing and UI dispatching.

### Supported Platforms

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

### Usage

#### User Controls
The library contains a rich set of customized user controls which extend basic implementations of existing controls. Following screenshots of the SampleApp demonstrate the usege of some of the delivered controls. The coloring is pretty random and mainly used for debugging/development purposes. Since every control uses dynamic styles, you're free to override the default styles.
<p float="left">
<img src="https://raw.githubusercontent.com/thomasgalliker/CrossPlatformLibrary/develop/Images/Screenshot_SampleApp_Android.jpg" alt="SampleApp Android" height="500">
<img src="https://raw.githubusercontent.com/thomasgalliker/CrossPlatformLibrary/develop/Images/Screenshot_SampleApp_iOS.png" alt="SampleApp iOS" height="500">
</p>

##### Getting Started
In order to use the user controls of CrossPlatformLibrary, the styles used in these controls need to be initialized properly. For this reason, add following line of code just after the line `this.InitializeComponent();` in `App.xaml.cs`:

`
CrossPlatformLibrary.Forms.CrossPlatformLibrary.Init(this, "SampleApp.Theme");
`


```TODO: to be documented```

##### Known Problems
- `System.Collections.Generic.KeyNotFoundException: The resource 'Theme.Color.TextColor' is not present in the dictionary.` This error eventually appears if user controls of CrossPlatformLibrary are used without calling the `CrossPlatformLibrary.Forms.Init(..)` method.

#### Input Validation
The base viewmodel ```BaseViewModel``` implements a pretty sophisiticated and praxisproven user input validation system which allows to run client- and server-based property validation side-by-side.
There are a few steps to follow to get input validation to work:

- Inherit your viewmodels from ```BaseViewModel``` or implement a similar logic which exposes a ```BaseViewModel.Validation``` property.
- Override the protected method ```SetupValidation```. This enables your viewmodel to use input validation. The most simple setup just returns an empty  ```ViewModelValidation```.
- Setup validation rules inside ```SetupValidation```. There are basically two different approaches: Either you validate viewmodel properties locally (validation logic provided by the viewmodel) or you call a backend service which validates a given object (DTO) against some central validation logic.
- Configure the according view to react on validation errors. This is done in XAML by binding a dependency property to the string list of validation errors for a certain property. The following example binds to validation errors for property 'UserName': ```ValidationErrors="{Binding Validation.Errors[UserName]}"```
- In order to run the validation, just call ```Validation.IsValidAsync()```. Depending on the result (true/false) we proceed with further actions (e.g. saving the object).
```
  var isValid = await this.Validation.IsValidAsync();
  if (isValid)
  {
      // TODO Save...
  }
```

Following snippet is an extract of a unit test. It demonstrates some setup variations.
```
protected override ViewModelValidation SetupValidation()
{
    var viewModelValidation = new ViewModelValidation();

    // Validation function with parameter-less custom error message
    viewModelValidation.AddValidationFor(nameof(this.UserName))
        .When(() => string.IsNullOrEmpty(this.UserName))
        .Show(() => "Username must not be empty");

    // Validation rule with parameter and custom error message
    viewModelValidation.AddValidationFor(nameof(this.Email))
        .When(new IsNotNullOrEmptyRule())
        .Show(p => $"Email address '{p}' must not be empty.");

    // Validation delegated to async service
    viewModelValidation.AddDelegateValidation(nameof(this.UserName), nameof(this.Email))
        .Validate(async () => (await this.validationService.ValidatePersonAsync(this.CreatePerson())).Errors);

    return viewModelValidation;
}
```

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

### Contribution

Want to contribute to this project? Feel free to start a discussion in the issues area to see if your idea could fit in.

### License

CrossPlatformLibrary is Copyright &copy; 2019 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.

