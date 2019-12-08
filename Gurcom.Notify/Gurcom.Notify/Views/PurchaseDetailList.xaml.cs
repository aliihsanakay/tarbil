using BildirimService;
using Gurcom.Notify.Models.DbModels;
using Gurcom.Notify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gurcom.Notify.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseDetailList : ContentPage
    {
        public  bool IsBusy { get; set; }
        public TransactionDetailResult[] Items { get; set; }
        public TransactionResult _transactionItem { get; set; }
        public PurchaseDetailList(TransactionResult transactionItem)
        {
            InitializeComponent();

            TransactionDetailListRequest request = new TransactionDetailListRequest();
            request.IdTransaction = transactionItem.ID_TRANSACTION;
            _transactionItem = transactionItem;

            BindingContext = this;
            var result = WebServiceLayer.BildirimClient.TransactionDetailList(request);

            Items = result.TransactionDetailResult;
            MyListView.ItemsSource = result.TransactionDetailResult;
        }
        async void GenerateXml_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var companySetting = SQLHelper.SqlLiteHelper.GetAll<Company>().LastOrDefault();

            List<Carrier> carrierList = new List<Carrier>();
            foreach (var item in Items)
            {
                Carrier carrier = new Carrier();
                carrier.CarrierLabel = !string.IsNullOrEmpty(item.CARRIER_LABEL1) ? item.CARRIER_LABEL1 : item.CARRIER_LABEL2;
                carrier.ContainerType = !string.IsNullOrEmpty(item.CARRIER_LABEL1) ? "P" : "C";

                Product product = new Product();
                product.ExpessionDate = item.EXPIRATION_DATE;
                product.GTIN = item.BARKOD;
                product.LotNumber = item.LOT_NUMBER;
                product.ProductionDate = item.PRODUCTION_DATE;
                product.SerialNumbers = new List<string>();
                product.SerialNumbers.Add(item.SERIAL_NUMBER);

                if (carrierList != null && !carrierList.Any(x => x.CarrierLabel == carrier.CarrierLabel))
                {
                    carrier.ProductList = new List<Product>();
                    carrier.ProductList.Add(product);
                    carrierList.Add(carrier);
                }
                else
                {
                    var carrierItem = carrierList.FirstOrDefault(x => x.CarrierLabel == carrier.CarrierLabel);
                    if (carrierItem.ProductList.Any(x => x.LotNumber == product.LotNumber && product.GTIN == product.GTIN && product.ProductionDate == product.ProductionDate && product.ExpessionDate == product.ExpessionDate))
                    {
                        var productItem = carrierItem.ProductList.FirstOrDefault(x => x.LotNumber == product.LotNumber && product.GTIN == product.GTIN && product.ProductionDate == product.ProductionDate && product.ExpessionDate == product.ExpessionDate);
                        productItem.SerialNumbers.Add(item.SERIAL_NUMBER);
                    }
                    else
                    {
                        carrierItem.ProductList.Add(product);
                    }
                }
            }


            string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            xml += "<transfer xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
            xml += "<receiver>" + companySetting.GlnNo + "</receiver>"; //kendisi
            xml += "<sender>" + _transactionItem.SENDER + "</sender>";//--malı aldığı kişi
            xml += "<key>" + companySetting.KeyNo + "</key>";
            xml += "<actionType>P</actionType> ";//--p alış S-satış
            xml += "<documentNumber>475488</documentNumber>";//--kendimiz vereceğiz
            xml += "<documentDate>" + _transactionItem.DOCUMENT_DATE + "</documentDate>";
            foreach (var item in carrierList)
            {
                xml += "<carrier carrierLabel=\"" + item.CarrierLabel + "\" containerType=\"" + item.ContainerType + "\">";// ---C Koli P:palet 
                foreach (var productItem in item.ProductList)
                {
                    xml += "<productList GTIN=\"" + productItem.GTIN + "\" lotNumber=\"" + productItem.LotNumber + "\" productionDate=\"" + productItem.ProductionDate + "\" expirationDate=\"" + productItem.ExpessionDate + "\">";
                    foreach (var serialNumberItem in productItem.SerialNumbers)
                    {
                        xml += "<serialNumber>" + serialNumberItem + "</serialNumber>";
                    }
                    xml += "</productList>";
                }
                xml += "</carrier>";
            }
            xml += "</transfer>";

            SendXMLNotification_DealerRequest request = new SendXMLNotification_DealerRequest();
            request.XMLText = xml;
            var result = WebServiceLayer.BildirimClient.SendXMLNotification_Dealer(request);
            if (!string.IsNullOrEmpty(result.NotificationResultGlobal.GLOBAL_ERROR_MSG))
                await DisplayAlert("Uyarı", result.NotificationResultGlobal.GLOBAL_ERROR_MSG, "Tamam");
            else
            {
                if (result.NotificationResultSerial.Count() > 0)
                {
                    await DisplayAlert("Sonuç", result.NotificationResultSerial[0].TRANSACTION_MESSAGE, "Tamam");
                }
            }
            IsBusy = false;
        }
        class Product
        {
            public string GTIN { get; set; }
            public string LotNumber { get; set; }
            public string ProductionDate { get; set; }
            public string ExpessionDate { get; set; }
            public List<string> SerialNumbers { get; set; }

        }
        class Carrier
        {
            public string CarrierLabel { get; set; }
            public string ContainerType { get; set; }
            public List<Product> ProductList { get; set; }
        }


    }
}
