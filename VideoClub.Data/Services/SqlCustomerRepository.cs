using System.Collections.Generic;
using System.Linq;
using VideoClub.Data.Context;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlCustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users;
        }

        public User Get(string id)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public void Add(User customer)
        {
            _dbContext.Users.Add(customer);
            _dbContext.SaveChanges();
        }
    }
}