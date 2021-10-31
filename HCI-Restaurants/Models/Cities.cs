using System;
using System.Collections.Generic;

namespace HCI_Restaurants.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Restaurants = new HashSet<Restaurants>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string FullName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<Restaurants> Restaurants { get; set; }
    }
}
