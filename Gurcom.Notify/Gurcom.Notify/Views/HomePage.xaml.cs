using Gurcom.Notify.Models;
using Gurcom.Notify.Models.DbModels;
using Gurcom.Notify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gurcom.Notify.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage :ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public HomePage()
        {InitializeComponent();
           
            
  
            BindingContext = this;
              

         
        }

        async void BtnMusteriler_ClickedAsync(object sender, EventArgs e)
        {

            //Customer customer = new Customer();
            //customer.Name = "Ali İhsan";
            //customer.Surname = "Akay";
            //customer.FatherName = "Ali";
            //customer.CreateDate = DateTime.Now;
            //customer.Address = "Antalya/Kaş";
            //var a1 = SQLHelper.SqlLiteHelper.Insert<Customer>(customer);
            //var a2 = SQLHelper.SqlLiteHelper.GetAll<Customer>();

          //  SQLHelper.SqlLiteHelper.DeleteTable<Customer>();

           // var id = (int)((HomeMenuItem)new HomeMenuItem { Id = MenuItemType.Customer, Title = "Müşteriler" }).Id;
           //await RootPage.NavigateFromMenu(id);
            await Navigation.PushAsync(new CustomersPage());
        }

        async void BtnCompany_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanySetting());
          //  await RootPage.NavigateFromMenu((int)MenuItemType.Company);
        }
        async void BtnAlisBildirimi_ClickedAsync(object sender,EventArgs e)
        {
         
            await Navigation.PushAsync(new PurchaseList());
        }
    }
}