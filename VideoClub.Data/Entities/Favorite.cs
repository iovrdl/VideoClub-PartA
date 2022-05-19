namespace VideoClub.Data.Entities
{
    public class Favorite
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Genre Genre { get; set; }
    }
}