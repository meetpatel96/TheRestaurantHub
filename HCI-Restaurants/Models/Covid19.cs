using System;
using System.Collections.Generic;

namespace HCI_Restaurants.Models
{
    public partial class Covid19
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string TakeOut { get; set; }
        public string LimitSeating { get; set; }
        public string IndoorDining { get; set; }
        public string Curbside { get; set; }
        public string Comments { get; set; }

        public virtual Restaurants Restaurant { get; set; }
    }
}
