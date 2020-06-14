using System.ComponentModel;
using GuardedActionsSample.ViewModels;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActionsSample.Pages
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = Startup.Services.GetService<MainViewModel>();
        }
    }
}
