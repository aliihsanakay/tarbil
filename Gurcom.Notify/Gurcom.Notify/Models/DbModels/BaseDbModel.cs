using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Gurcom.Notify.Models.DbModels
{
   public class BaseDbModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool Deleted { get; set; }
        public BaseDbModel()
        {
            CreateDate = DateTime.Now;
        }
    }
}
