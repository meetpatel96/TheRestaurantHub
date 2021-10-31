using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HCI_Restaurants.Models
{
    public partial class Restaurants
    {
        public Restaurants()
        {
            Covid19 = new HashSet<Covid19>();
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public int CityId { get; set; }
        public int CuisineId { get; set; }
        public string Cuisines { get; set; }
        public string Currency { get; set; }
        public string Establishment { get; set; }
        [DisplayName ("Delivery")]
        public string HasDelivery { get; set; }
        [DisplayName("Takeout")]
        public string HasTakeaway { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [DisplayName("State Code")]
        public string StateCode { get; set; }
        public string Locality { get; set; }
        [DisplayName("Locality Verbose")]
        public string LocalityVerbose { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Menu")]
        public string MenuUrl { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        [DisplayName("Price Range")]
        public long? PriceRange { get; set; }
        [DisplayName("Hours")]
        public string Timings { get; set; }
        public string Url { get; set; }
        [DisplayName("Ratings")]
        public string AggregateRating { get; set; }
        [DisplayName("Comments")]
        public string RatingText { get; set; }

        public virtual Cities CityNavigation { get; set; }
        public virtual Cuisines Cuisine { get; set; }
        public virtual ICollection<Covid19> Covid19 { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
