using System.Collections.Generic;

namespace VideoClub.Data.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public List<Copy> Copies { get; set; }

        public Film()
        {
            Copies = new List<Copy>();
        }
    }
}