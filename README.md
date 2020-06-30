# GuardedActions
A library to :rocket: increase the :x: error handling, :test_tube: testability and :recycle: reusability for all your MVVM driven apps! More information about these topics and why you should use the GuardedActions library can be found [here](#so-why-should-i-use-it).

The Guarded Actions library comes with a set of providers to support some of the most commonly used of the IoC containers. 

| IoC container | Supported |
| ------------- | ------------- |
| [.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/) | :white_check_mark: |
| [MvvmCross](https://www.mvvmcross.com/) | :white_check_mark: |
| [Unity](http://unitycontainer.org/) | :construction: |
| [Autofac](https://autofac.org/) | :construction: |
| [TinyIoC](https://github.com/grumpydev/TinyIoC) | :construction: |
| Custom (read more below) | :white_check_mark: |

<!-- | [Ninject](http://www.ninject.org/) | :construction: | -->
<!-- | [Castle.Windsor](http://www.castleproject.org/projects/windsor/) | :construction: | -->

 <!-- Also, it'll be possible to extend the Guarded Actions library to your needs as it comes with the possibility of creating your own IoC provider. This way you could connect the GuardedActions Library to any IoC provider of your wishes! -->

## So why should I use it?

### Reusability

The command builders make it really easy to reuse commands troughout multiple view models without having to create some kind of base class like shown below: 

Classic example:
```csharp
public class ViewModelA : SharedViewModel
{
}

public class ViewModelB : SharedViewModel
{
}

public class SharedViewModel : BaseViewModel
{
    private ICommand _scanCommand;
    public ICommand ScanCommand => _scanCommand ??= new Command(Scan);
    private void Scan()
    {
        ...
    }
}
```

The solution above could cause some issues in bigger projects. In these projects people tend to reuse specific commands. The most common approach we see people take is creating somekind of base class containing the shared commands. 

Now in big projects this shared class tend to grow quickly and become some big spagetti class with all kind of commands for different purposes.. 

Not really following the separation of concerns design priciples.. 

So, this means that if you only need one or a couple of the commands you'll need to inherit the entire base class with all kinds of stuff you don't want/need or you'll create a copy with only those commands you like to use. But keep in mind when you copy past commands you don't reuse them.. 

So with the Guarded actions you could solve this issue by creating `CommandBuilders` which can be loaded trough DI and so are reuseable. Note: you don't need to register the builders yourself if you [install](#installation) the `GuardedActions` library correctly it'll register and resolve the builders automatically.

GuardedAction code:

```csharp
    public class ViewModelA : BaseViewModel, IScannable
    {
        private readonly IScanCommandBuilder _scanCommandBuilder;
        
        private ICommand _scanCommand;
        public ICommand ScanCommand => _scanCommand ??= _scanCommandBuilder?.RegisterDataContext(this).BuildCommand()
        
        public ViewModelA(IScanCommandBuilder scanCommandBuilder)
        {
            _scanCommandBuilder = scanCommandBuilder ?? throw new ArgumentNullException(nameof(scanCommandBuilder));
        }
    }
    
    public class ViewModelB : BaseViewModel, IScannable
    {
        private readonly IScanCommandBuilder _scanCommandBuilder;
        
        private ICommand _scanCommand;
        public ICommand ScanCommand => _scanCommand ??= _scanCommandBuilder?.RegisterDataContext(this).BuildCommand()
        
        public ViewModelA(IScanCommandBuilder scanCommandBuilder)
        {
            _scanCommandBuilder = scanCommandBuilder ?? throw new ArgumentNullException(nameof(scanCommandBuilder));
        }
    }
    
    public class ScanCommandBuilder : AsyncGuardedDataContextCommandBuilder<IScannable>, IScanCommandBuilder
    {
        protected override Task ExecuteCommandAction()
        {
            // 1. Multiple actions can be added handling the scanning feature
            // 2. The DataContext could be modified directly. This is in this case the ViewModel but accessed trough it's IScannable contract.
            return Task.CompletedTask;
        }
    }
```

### Testability
:construction: Under construction, coming ASAP! :construction:

### Error handling
:construction: Under construction, coming ASAP! :construction:

## Installation

Different IoC containers need different providers and so different NuGet packages. Down here you'll see samples on how to setup each IoC container provider.

 - [.NET Core](#net-core)
 - [MvvmCross](#mvvmcross)
 - [Custom](#custom)

:construction: The rest is to coming soon! :construction:

### .NET Core

Grab the latest [GuardedActions.NetCore NuGet](https://www.nuget.org/packages/GuardActions.NetCore/) package and install in your solution.
> Install-Package GuardedActions.NetCore

Then the only thing you've to do is configuring and connect the GuardedActions library on the host builder. See the example below: 

```csharp
using GuardedActions.NetCore;
using GuardedActions.NetCore.Extensions;

public class Startup
{
    public static void Init()
    {
        var iocSetup = new GuardedActionsIoCSetup();

        var host = new HostBuilder()
            .ConfigureGuardedActions(iocSetup, "YourAssembliesStartWith")
            .Build()
            .ConnectGuardedActions(iocSetup);
    }
}
```

### MvvmCross

Grab the latest [GuardedActions.MvvmCross NuGet](https://www.nuget.org/packages/GuardActions.MvvmCross/) package and install in your solution.
> Install-Package GuardedActions.MvvmCross

Then the only thing you've to do is configuring GuardedActions before registering the AppStart. See the example below: 

```csharp
using GuardedActions.MvvmCross;

public class App : MvxApplication
{
    public override void Initialize()
    {
        new GuardedActionsIoCSetup().Configure(Mvx.IoCProvider, "YourAssembliesStartWith");

        RegisterAppStart<MainViewModel>();
    }
}
```

### Custom

Grab the latest [GuardedActions NuGet](https://www.nuget.org/packages/GuardActions/) package and install in your solution.
> Install-Package GuardedActions

Then the only thing you've to do is to create your own IoC setup class in which you will connect your IoC container to the GuardedActions library. This can be done by creating a `GuardedActionCustomIoCSetup` class which inherits from the `GuardedActions.IoC.IoCRegistraton` class. See the example below: 

```csharp
using GuardedActions.IoC;

public class GuardedActionCustomIoCSetup : IoCRegistration
{
    private IYourIoCContainer? _yourIoCContainer;

    private static string _customErrorMessage = $"Make sure you've called the {nameof(Configure)} on the {nameof(GuardedActionCustomIoCSetup)} before your app starts.";

    public void Configure(IYourIoCContainer yourIoCContainer, params string[] assemblyNames)
    {
        _yourIoCContainer = yourIoCContainer ?? throw new ArgumentNullException(nameof(yourIoCContainer));
            
        Register(assemblyNames);
    }

    public override void AddSingletonInternal<TServiceType>(Func<TServiceType> constructor) where TServiceType : class => _yourIoCContainer.AddSingleton(() => constructor.Invoke());

    public override void AddSingletonInternal(Type serviceType) => _yourIoCContainer.AddSingleton(serviceType);

    public override void AddSingletonInternal(Type contractType, Type serviceType) => _yourIoCContainer.AddSingleton(contractType, serviceType);

    public override void AddTransientInternal(Type serviceType) => _yourIoCContainer.AddTransient(serviceType);

    public override void AddTransientInternal(Type contractType, Type serviceType) => _yourIoCContainer.AddTransient(contractType, serviceType);

    public override TServiceType GetServiceInternal<TServiceType>() where TServiceType : class => _yourIoCContainer.GetService<TServiceType>();

    public override TServiceType GetServiceInternal<TServiceType>(Type serviceType) where TServiceType : class => _yourIoCContainer.GetService<TServiceType>(serviceType);

    public override bool CanRegister => _yourIoCContainer != null;

    public override bool CanResolve => _yourIoCContainer != null;

    public override string CannotRegisterErrorMessage => _customErrorMessage;

    public override string CannotResolveErrorMessage => _customErrorMessage;
}
```

And then of course don't forget to call your custom IoC setup class after setting up your IoC container and before loading your app.


```csharp
new GuardedActionCustomIoCSetup().Configure(yourIoCContainer, "YourAssembliesStartWith");

```

## Filing issues

When filing issues, please select the appropriate [issue template](https://github.com/Baseflow/GuardedActions/issues/new/choose). The best way to get your bug fixed is to be as detailed as you can be about the problem.
Providing a minimal git repository with a project showing how to reproduce the problem is ideal. Here are a couple of questions you can answer before filing a bug.

1. Did you include a snippet of the broken code in the issue?
2. Can you reproduce the problem in a brand new project?
3. What are the _*EXACT*_ steps to reproduce this problem?
4. What platform(s) are you experiencing the problem on?

Remember GitHub issues support [markdown](https://github.github.com/github-flavored-markdown/). When filing bugs please make sure you check the formatting of the issue before clicking submit.

## Contributing code

We are happy to receive Pull Requests adding new features and solving bugs. As for new features, please contact us before doing major work. To ensure you are not working on something that will be rejected due to not fitting into the roadmap or ideal of the library.

### Git setup

Since Windows and UNIX-based systems differ in terms of line endings, it is a very good idea to configure git autocrlf settings.

On *Windows* we recommend setting `core.autocrlf` to `true`.

```
git config --global core.autocrlf true
```

On *Mac* we recommend setting `core.autocrlf` to `input`.

```
git config --global core.autocrlf input
```

### Code style guidelines

Please use 4 spaces for indentation.

Otherwise default ReSharper C# code style applies.

### Project Workflow

Our workflow is loosely based on [Github Flow](http://scottchacon.com/2011/08/31/github-flow.html).
We actively do development on the **develop** branch. This means that all pull requests by contributors need to be develop and requested against the develop branch.
The master branch contains tags reflecting what is currently on NuGet.org.

### Submitting Pull Requests

Make sure you can build the code. Familiarize yourself with the project workflow and our coding conventions. If you don't know what a pull request is
read this https://help.github.com/articles/using-pull-requests.

Before submitting a feature or substantial code contribution please discuss it with the team and ensure it follows the GuardedAction roadmap.
Note that code submissions will be reviewed and tested. Only code that meets quality and design/roadmap appropriateness will be merged into the source. [Don't "Push" Your Pull Requests](https://www.igvita.com/2011/12/19/dont-push-your-pull-requests/)

### Acknowledgements

* Thanks to [Artur Malendowicz](https://github.com/Immons) for some ideas / code on which this library is based upon.
