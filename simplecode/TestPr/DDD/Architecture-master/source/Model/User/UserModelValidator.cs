using DotNetCore.Validation;
using FluentValidation;

namespace Architecture.Model
{
    public abstract class UserModelValidator : Validator<UserModel>
    {
        public void Auth()
        {
            RuleFor(x => x.Auth).SetValidator(new AuthModelValidator());
        }

        public void Email()
        {
            RuleFor(x => x.Email).EmailAddress();
        }

        public void Forename()
        {
            RuleFor(x => x.Forename).NotEmpty();
        }

        public void Id()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        public void Surname()
        {
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
