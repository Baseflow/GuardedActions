using System.Threading.Tasks;
using GuardedActions.Commands;
using GuardedActionsSample.Commands.Main.Interfaces;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Main
{
    public class DownloadAllCommandBuilder : AsyncGuardedDataContextCommandBuilder<MainViewModel>, IDownloadAllCommandBuilder
    {
        protected override Task ExecuteCommandAction()
        {
            foreach (var download in DataContext.Downloads)
            {
                download.DownloadAction.ExecuteGuarded();
            }
            return Task.CompletedTask;
        }
    }
}
