using System;
using System.Collections.Generic;

namespace HCI_Restaurants.Models
{
    public partial class Cuisines
    {
        public Cuisines()
        {
            Restaurants = new HashSet<Restaurants>();
        }

        public int Id { get; set; }
        public string CuisineName { get; set; }

        public virtual ICollection<Restaurants> Restaurants { get; set; }
    }
}
