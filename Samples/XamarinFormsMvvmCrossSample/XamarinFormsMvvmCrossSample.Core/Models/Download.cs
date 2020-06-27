using System;
using AsyncAwaitBestPractices.MVVM;
using XamarinFormsMvvmCrossSample.Core.Commands.Contracts;

namespace XamarinFormsMvvmCrossSample.Core.Models
{
    public class Download : NotifyPropertyChanged
    {
        private string? _errorMessage;
        private bool _isDownloaded;

        public Download(string url, IDownloadCommandBuilder downloadCommandBuilder)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));

            if (downloadCommandBuilder == null)
                throw new ArgumentNullException(nameof(downloadCommandBuilder));

            DownloadCommand = downloadCommandBuilder.RegisterDataContext(this).BuildCommand();
        }

        public string? ErrorMessage
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

        public IAsyncCommand DownloadCommand { get; }
    }
}
