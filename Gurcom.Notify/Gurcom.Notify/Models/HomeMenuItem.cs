using System;
using System.Collections.Generic;
using System.Text;

namespace Gurcom.Notify.Models
{
    public enum MenuItemType
    {
        HomePage,
        Customer,
        Purchase,
        Company,
        Login
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
