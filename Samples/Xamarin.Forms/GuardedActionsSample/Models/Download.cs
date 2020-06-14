using GuardedActionsSample.Actions.Main.Interfaces;

namespace GuardedActionsSample.Models
{
    public class Download : NotifyPropertyChanged
    {
        private string _errorMessage;
        private bool _isDownloaded;

        public Download(string url, IDownloadUrlAction downloadAction)
        {
            Url = url;

            DownloadAction = downloadAction;
            DownloadAction?.RegisterDataContext(this);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value, () => RaisePropertyChanged(nameof(HasError)));
        }

        public bool IsDownloaded
        {
            get => _isDownloaded;
            set => SetProperty(ref _isDownloaded, value);
        }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        public string Url { get; }

        public IDownloadUrlAction DownloadAction { get; }
    }
}
