using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeWeightt.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Test
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
