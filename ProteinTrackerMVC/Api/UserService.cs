using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace ProteinTrackerMVC.Api
{
    public class UserService : Service
    {
        public IRepository Repository { get; set; }

        public object Post(AddUser request)
        {
            var id = Repository.AddUser(request.Name, request.Goal);

            return new AddUserResponse {UserId = 255};
        }
    }

    [Route("/users", "POST")]
    public class AddUser
    {
        public string Name { get; set; }
        public int Goal { get; set; }
    }

    public class AddUserResponse
    {
        public long UserId { get; set; }
    }
}


