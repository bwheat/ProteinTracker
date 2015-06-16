using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace ProteinTrackerMVC.Api
{
    public interface IRepository
    {
        long AddUser(string name, int goal);
    }

    public class Repository : IRepository
    {
        private IRedisClientsManager redisManager { get; set; }

        public Repository(IRedisClientsManager manager)
        {
            redisManager = manager;
        }

        public long AddUser(string name, int goal)
        {
            using (var redisClient = redisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                var user = new User {Name = name, Goal = goal, Id = redisUsers.GetNextSequence()};
                redisUsers.Store(user);
                return user.Id;
            }
        }
    }
}
