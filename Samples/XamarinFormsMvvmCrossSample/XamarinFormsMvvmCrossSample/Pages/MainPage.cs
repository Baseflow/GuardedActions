using System;
using System.Linq;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using XamarinFormsMvvmCrossSample.Core.ViewModels;

namespace XamarinFormsMvvmCrossSample.Pages
{
    public class MainPage : MvxContentPage<MainViewModel>
    {
        public MainPage()
        {
            //         <StackLayout HorizontalOptions="Center">
            //    <StackLayout BindableLayout.ItemsSource="{Binding Downloads}">
            //        <BindableLayout.ItemTemplate>
            //            <DataTemplate>
            //                <Grid>
            //                    <Grid.ColumnDefinitions>
            //                        <ColumnDefinition Width="*" />
            //                        <ColumnDefinition Width="Auto" />
            //                    </Grid.ColumnDefinitions>
            //                    <Grid.RowDefinitions>
            //                        <RowDefinition Height="Auto" />
            //                    </Grid.RowDefinitions>
            //                    <Label Text="{Binding Url}" HorizontalOptions="FillAndExpand" />
            //                    <Label Grid.Column="1" Text="X" TextColor="Red" IsVisible="{Binding HasError}" />
            //                </Grid>
            //            </DataTemplate>
            //        </BindableLayout.ItemTemplate>
            //    </StackLayout>

            //    <Button Text="Download all" Command="{Binding DownloadAllCommand}" />
            //</StackLayout>

            var downloadsStackLayout = new ListView
            {
                BindingContext = ViewModel,
                ItemTemplate = new DataTemplate(() =>
                {
                    return new Label { Text = "Jop test 2" };
                    var urlLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
                    var errorIndicatorLabel = new Label { Text = "X", TextColor = Color.Red };

                    urlLabel.SetBinding(Label.TextProperty, "Url");
                    urlLabel.SetBinding(Label.IsVisibleProperty, "HasError");

                    var grid = new Grid
                    {
                        ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto },
                    },
                        RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = GridLength.Auto },
                    },
                        Children = {
                        urlLabel,
                        errorIndicatorLabel
                    }
                    };

                    Grid.SetColumn(errorIndicatorLabel, 1);

                    return grid;
                })
            };
            downloadsStackLayout.SetBinding(ListView.ItemsSourceProperty, "Downloads");

            //ListView.SetItemTemplate(downloadsStackLayout, new DataTemplate(() =>
            //{
            //    return new Label { Text = "Jop test 2" };
            //    var urlLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
            //    var errorIndicatorLabel = new Label { Text = "X", TextColor = Color.Red };

            //    urlLabel.SetBinding(Label.TextProperty, "Url");
            //    urlLabel.SetBinding(Label.IsVisibleProperty, "HasError");

            //    var grid = new Grid
            //    {
            //        ColumnDefinitions = new ColumnDefinitionCollection
            //        {
            //            new ColumnDefinition { Width = GridLength.Star },
            //            new ColumnDefinition { Width = GridLength.Auto },
            //        },
            //        RowDefinitions = new RowDefinitionCollection
            //        {
            //            new RowDefinition { Height = GridLength.Auto },
            //        },
            //        Children = {
            //            urlLabel,
            //            errorIndicatorLabel
            //        }
            //    };

            //    Grid.SetColumn(errorIndicatorLabel, 1);

            //    return grid;
            //}));

            Content = new StackLayout
            {
                BindingContext = ViewModel,
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new Label { Text = "Jop test" },
                    downloadsStackLayout,
                }
            };
        }
    }
}

