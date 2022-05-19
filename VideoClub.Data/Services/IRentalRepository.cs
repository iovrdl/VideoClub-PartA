using System.Collections.Generic;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetAll();
        Rental Get(int id);
        void Add(Rental rental);
        void Update(Rental rental);
    }
}