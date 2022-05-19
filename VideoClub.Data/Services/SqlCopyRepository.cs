using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoClub.Data.Context;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public class SqlCopyRepository : ICopyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlCopyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Copy> Get(Film film)
        {
            return _dbContext.Copies.Where(copy => copy.FilmId.Equals(film.Id));
        }

        public Copy Get(int id)
        {
            return _dbContext.Copies.FirstOrDefault(copy => copy.Id.Equals(id));
        }

        public void Add(Copy copy)
        {
            _dbContext.Copies.Add(copy);
            _dbContext.SaveChanges();
        }

        public void Update(Copy copy)
        {
            var entry = _dbContext.Entry(copy);
            entry.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}