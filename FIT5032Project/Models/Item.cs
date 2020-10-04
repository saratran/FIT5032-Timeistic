using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime EndTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
       /* [DefaultValue(DateTime.Now)]*/
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

/*    public class ItemTracker
    {
        public Item NewItem { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }*/
}