using System.Collections.Generic;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<User> GetAll();
        User Get(string id);
        void Add(User customer);
    }
}