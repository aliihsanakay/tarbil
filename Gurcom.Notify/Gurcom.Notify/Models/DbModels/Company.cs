using System;
using System.Collections.Generic;
using System.Text;

namespace Gurcom.Notify.Models.DbModels
{
    public class Company:BaseDbModel
    {
        public string CompanyName { get; set; }
        public string GlnNo { get; set; }
        public string KeyNo { get; set; }
    }
}
