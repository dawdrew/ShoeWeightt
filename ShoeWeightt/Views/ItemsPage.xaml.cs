using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ShoeWeightt.Models;
using ShoeWeightt.Views;
using ShoeWeightt.ViewModels;


namespace ShoeWeightt.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            //viewModel.Items.Remove(item); SETH CODE
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        async public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            await DisplayAlert("Delete Browse Item", mi.CommandParameter + " will be removed from list", "OK");
            var listItem = (from itm in await App.Database.GetItemAsync()
                            where itm.Text == mi.CommandParameter.ToString()
                            select itm.Id).FirstOrDefault();
            Console.WriteLine(listItem.ToString());
            //viewModel.Items.Remove(listItem);
            //await viewModel.DataStore.DeleteItemAsync(listItem);
            await App.Database.DeleteItemAsync(await App.Database.GetItemAsync(listItem));
            ItemsListView.ItemsSource = await App.Database.GetItemAsync();

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ItemsListView.ItemsSource = await App.Database.GetItemAsync();
            //viewModel.DataStore =
            //viewModel.LoadItemsCommand.Execute(ItemsListView);
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(true);
        }
    }
}

