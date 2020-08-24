namespace Architecture.Model
{
    public sealed class AddUserModelValidator : UserModelValidator
    {
        public AddUserModelValidator()
        {
            Forename();
            Surname();
            Email();
            Auth();
        }
    }
}
