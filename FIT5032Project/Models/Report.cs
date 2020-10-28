using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032Project.Models
{
    public class Report
    {
        IList<Item> items { get; set; }

        public Report(IList<Item> items)
        {
            this.items = items;
        }
    }
}