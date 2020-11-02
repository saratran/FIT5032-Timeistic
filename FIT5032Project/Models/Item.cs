using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? Rating { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime EndTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
       /* [DefaultValue(DateTime.Now)]*/
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public virtual Location Location { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual ApplicationUser User { get; set; }
    }

/*    public class ItemTracker
    {
        public Item NewItem { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }*/
}