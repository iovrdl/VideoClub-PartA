using System.Collections.Generic;
using VideoClub.Data.Entities;

namespace VideoClub.Web.Models
{
    public class UnreturnedRentalsViewModel
    {
        public User User { get; set; }
        public List<Rental> UnreturnedRentals { get; set; }
    }
}