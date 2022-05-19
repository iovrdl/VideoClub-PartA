using PagedList;
using VideoClub.Data.Entities;

namespace VideoClub.Web.Models
{
    public class FilmsBindModel
    {
        public Genre? Genre { get; set; }
        public IPagedList<Film> Films { get; set; }
    }
}