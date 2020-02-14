using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ShoeWeightt.Models;

namespace ShoeWeightt.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Id = 0,
                Text = "Item name",
                Description = "This is an item description."
                
            };
            //Console.WriteLine("please work");
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddItem", Item);
            //Console.WriteLine(Item.Text.ToString(), Item.Description.ToString());
            //var item = (Item)BindingContext;
            //Console.WriteLine(item.ToString(),"penis monster lives here");
            await App.Database.SaveItemAsync(Item);
            await Navigation.PopModalAsync();

        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}