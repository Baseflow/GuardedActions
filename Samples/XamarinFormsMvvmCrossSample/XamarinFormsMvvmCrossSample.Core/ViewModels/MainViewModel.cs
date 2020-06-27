using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinFormsMvvmCrossSample.Core.Commands.Contracts;
using XamarinFormsMvvmCrossSample.Core.Models;

namespace XamarinFormsMvvmCrossSample.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _errorMessage;

        private IEnumerable<Download> _downloads;

        private readonly IInitializeCommandBuilder _initializeCommandBuilder;
        private readonly IDownloadAllCommandBuilder _downloadAllCommandBuilder;

        private ICommand _initializeCommand;
        private ICommand _downloadAllCommand;

        public MainViewModel(IInitializeCommandBuilder initializeCommandBuilder, IDownloadAllCommandBuilder downloadAllCommandBuilder)
        {
            _initializeCommandBuilder = initializeCommandBuilder ?? throw new ArgumentNullException(nameof(initializeCommandBuilder));
            _downloadAllCommandBuilder = downloadAllCommandBuilder ?? throw new ArgumentNullException(nameof(downloadAllCommandBuilder));

            InitializeCommand.Execute(null);
        }

        public ICommand InitializeCommand => _initializeCommand ??= _initializeCommandBuilder?.RegisterDataContext(this).BuildCommand();
        public ICommand DownloadAllCommand => _downloadAllCommand ??= _downloadAllCommandBuilder?.RegisterDataContext(this).BuildCommand();

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public IEnumerable<Download> Downloads
        {
            get => _downloads;
            set => SetProperty(ref _downloads, value);
        }
    }
}
