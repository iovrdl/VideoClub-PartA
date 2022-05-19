using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoClub.Data.Context;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public class SqlRentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlRentalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Rental> GetAll()
        {
            return _dbContext.Rentals
                .Include(r => r.Film)
                .Include(r => r.User);
        }

        public Rental Get(int id)
        {
            return _dbContext.Rentals
                .Include(r => r.Film)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(Rental rental)
        {
            _dbContext.Rentals.Add(rental);
            _dbContext.SaveChanges();
        }

        public void Update(Rental rental)
        {
            var entry = _dbContext.Entry(rental);
            entry.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}