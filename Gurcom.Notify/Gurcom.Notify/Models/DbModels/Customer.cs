using System;
using System.Collections.Generic;
using System.Text;

namespace Gurcom.Notify.Models.DbModels
{
  public  class Customer: BaseDbModel
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentiyNumber { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        

    }
}
