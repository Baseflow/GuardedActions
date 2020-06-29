using System.ComponentModel;
using GuardedActionsPlayground.Core.ViewModels;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActionsPlayground.Pages
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
