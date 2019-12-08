using BildirimService;
using Gurcom.Notify.Models.DbModels;
using Gurcom.Notify.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gurcom.Notify.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseList : ContentPage
    {
        public ObservableCollection<TransactionResult> Items { get; set; }

        public PurchaseList()
        {
            InitializeComponent();
            var companySetting = SQLHelper.SqlLiteHelper.GetAll<Company>().LastOrDefault();
            TransactionListRequest request = new TransactionListRequest();
            request.DocumentNumber = null;
            request.EndDate = DateTime.Now;
            request.GLN_PN = companySetting.GlnNo;
            request.Key = companySetting.KeyNo;
            request.StartDate = DateTime.Now.AddYears(-1);

          var result= WebServiceLayer.BildirimClient.TransactionList(request);
           // WebServiceLayer.BildirimClient.SendXMLNotification_Dealer()


            //foreach (var item in result.TransactionResult)
            //{
            //    GetCompanyNameByGLN_PNRequest request1 = new GetCompanyNameByGLN_PNRequest();
            //    request1.GLN_PN = item.SENDER;
            //    var satanfirma = WebServiceLayer.BildirimClient.GetCompanyNameByGLN_PN(request1);
            //    item.SENDER = satanfirma.COMPANY_NAME;
            //}
            MyListView.ItemsSource = result.TransactionResult.OrderByDescending(x=>x.DOCUMENT_DATE);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var transactionResultItem = e.Item as TransactionResult;

            //GetCompanyNameByGLN_PNRequest request1 = new GetCompanyNameByGLN_PNRequest();
            //request1.GLN_PN = transactionResultItem.SENDER;
            //var satanfirma = WebServiceLayer.BildirimClient.GetCompanyNameByGLN_PN(request1);
            //transactionResultItem.SENDER = satanfirma.COMPANY_NAME;

            //await DisplayAlert("Item Tapped", transactionResultItem.SENDER, "OK");

       
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new PurchaseDetailList(transactionResultItem));

          
        }
    }
}
