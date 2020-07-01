---------------------------------
GuardedActions
---------------------------------

A library to increase the error handling, testability and reusability for all your MVVM driven apps!


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


---------------------------------
Star on Github if this project helps you: https://github.com/Baseflow/GuardedActions

Commercial support is available. Integration with your app or services, samples, feature request, etc. Email: hello@baseflow.com
Powered by: https://baseflow.com
---------------------------------