namespace FIT5032Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal Longitude { get; set; }
    }
}
