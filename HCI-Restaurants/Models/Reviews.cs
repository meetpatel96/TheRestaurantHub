using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCI_Restaurants.Models
{
    public partial class Reviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public long? Rating { get; set; }
        public string RatingText { get; set; }
        public string ReviewText { get; set; }
        public string ReviewTimeFriendly { get; set; }
        public string CustomerName { get; set; }

        public virtual Restaurants Restaurant { get; set; }
    }
}
