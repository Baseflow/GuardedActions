using GuardedActions.MvvmCross;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using XamarinFormsMvvmCrossSample.Core.Factories;
using XamarinFormsMvvmCrossSample.Core.Factories.Contracts;
using XamarinFormsMvvmCrossSample.Core.ViewModels;

namespace XamarinFormsMvvmCrossSample.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            ConfigureIoC();

            RegisterAppStart<MainViewModel>();
        }

        private void ConfigureIoC()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IDownloadFactory>(new DownloadFactory());

            new GuardedActionIoCSetup().Configure(Mvx.IoCProvider, nameof(XamarinFormsMvvmCrossSample));
        }
    }
}
