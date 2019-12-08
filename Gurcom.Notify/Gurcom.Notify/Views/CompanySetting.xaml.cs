using Gurcom.Notify.Models;
using Gurcom.Notify.Models.DbModels;
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
    public partial class CompanySetting : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public Company Item { get; set; }
        public CompanySetting()
        {
            InitializeComponent();
           
            Item = SQLHelper.SqlLiteHelper.GetAll<Company>().LastOrDefault();
            if (Item == null)
                Item = new Company();
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            //  MessagingCenter.Send(this, "AddCustomer", Item);
            SQLHelper.SqlLiteHelper.Insert<Company>(Item);
            await RootPage.NavigateFromMenu((int)MenuItemType.HomePage);
        }

    }
}