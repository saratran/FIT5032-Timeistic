using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032Project.Models
{

    public class TrackerViewModel
    {
        public Item NewItem { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}