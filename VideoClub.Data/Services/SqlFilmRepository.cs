using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VideoClub.Data.Context;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public class SqlFilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlFilmRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Film> GetAll()
        {
            return _dbContext.Films.Include(f => f.Copies);
        }

        public Film Get(int id)
        {
            return _dbContext.Films.Include(f => f.Copies).FirstOrDefault(f => f.Id == id);
        }

        public void Add(Film film)
        {
            _dbContext.Films.Add(film);
            _dbContext.SaveChanges();
        }
    }
}