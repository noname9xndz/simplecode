using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public class UserFactory : IUserFactory
    {
        public User Create(UserModel model, Auth auth)
        {
            return new User
            (
                new Name(model.Forename, model.Surname),
                new Email(model.Email),
                auth
            );
        }
    }
}
