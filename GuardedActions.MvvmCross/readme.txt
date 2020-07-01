---------------------------------
GuardedActions
---------------------------------

A library to increase the error handling, testability and reusability for all your MVVM driven apps!


using GuardedActions.MvvmCross;

public class App : MvxApplication
{
    public override void Initialize()
    {
        new GuardedActionsIoCSetup().Configure(Mvx.IoCProvider, "YourAssembliesStartWith");

        RegisterAppStart<MainViewModel>();
    }
}


---------------------------------
Star on Github if this project helps you: https://github.com/Baseflow/GuardedActions

Commercial support is available. Integration with your app or services, samples, feature request, etc. Email: hello@baseflow.com
Powered by: https://baseflow.com
---------------------------------