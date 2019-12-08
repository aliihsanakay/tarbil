using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Gurcom.Notify.Models;
using Gurcom.Notify.Views;
using Gurcom.Notify.Models.DbModels;
using System.Linq;

namespace Gurcom.Notify.ViewModels
{
    public class CustomersViewModel : BaseViewModel
    {
        public ObservableCollection<Customer> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CustomersViewModel()
        {
               Title = "Müşteriler";
            Items = new ObservableCollection<Customer>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewCustomerPage, Customer>(this, "AddCustomer", async (obj, item) =>
            {
                Customer newItem = item as Customer;
               
                var result = SQLHelper.SqlLiteHelper.Insert<Customer>(newItem);
                newItem.Name += " " + newItem.Surname;
                Items.Add(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = SQLHelper.SqlLiteHelper.GetAll<Customer>().OrderByDescending(x=>x.Id);
                foreach (var item in items)
                {
                    item.Name += " " + item.Surname;
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}