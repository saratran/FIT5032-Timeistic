using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FIT5032Project.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Priority { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class ItemTracker
    {
        public Item NewItem { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}