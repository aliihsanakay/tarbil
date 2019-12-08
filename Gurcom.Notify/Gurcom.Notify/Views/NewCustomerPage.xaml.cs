using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Gurcom.Notify.Models;
using Gurcom.Notify.Models.DbModels;

namespace Gurcom.Notify.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewCustomerPage : ContentPage
    {
        public Customer Item { get; set; }

        public NewCustomerPage()
        {
            InitializeComponent();


            Item = new Customer();
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
              MessagingCenter.Send(this, "AddCustomer", Item);       
          
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}