using System;
using System.Collections.Generic;
using VideoClub.Data.Entities;

namespace VideoClub.Web.Models
{
    public class CreateRentalBindModel
    {
        public int FilmId { get; set; }
        public string UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Note { get; set; }
        public List<Film> Films { get; set; }
        public List<User> Customers { get; set; }
    }
}