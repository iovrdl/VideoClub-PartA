using System.Collections.Generic;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public interface ICopyRepository
    {
        IEnumerable<Copy> Get(Film film);
        Copy Get(int id);
        void Add(Copy copy);
        void Update(Copy copy);
    }
}