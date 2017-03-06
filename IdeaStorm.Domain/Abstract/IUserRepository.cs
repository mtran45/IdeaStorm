using System.Collections.Generic;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        User GetUserByID(string userID);
    }
}
