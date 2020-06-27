using GuardedActions.Commands.Contracts;
using XamarinFormsMvvmCrossSample.Core.ViewModels;

namespace XamarinFormsMvvmCrossSample.Core.Commands.Contracts
{
    public interface IInitializeCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
