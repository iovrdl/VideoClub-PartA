using System.Collections.Generic;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetAll();
        Film Get(int id);
        void Add(Film film);
    }
}