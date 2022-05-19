using VideoClub.Data.Entities;

namespace VideoClub.Web.Models
{
    public class CreateFilmBindModel
    {
        public Film Film { get; set; }
        public int Copies { get; set; }
    }
}