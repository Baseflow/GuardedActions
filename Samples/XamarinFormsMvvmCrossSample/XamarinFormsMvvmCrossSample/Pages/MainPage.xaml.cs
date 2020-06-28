using System.ComponentModel;
using MvvmCross.Forms.Views;
using XamarinFormsMvvmCrossSample.Core.ViewModels;

namespace XamarinFormsMvvmCrossSample.Pages
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MvxContentPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
