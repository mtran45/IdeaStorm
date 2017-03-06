using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaStorm.Domain.Abstract;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context;

        public EFUserRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public User GetUserByID(string userID)
        {
            return context.Users.Find(userID);
        }
    }
}
