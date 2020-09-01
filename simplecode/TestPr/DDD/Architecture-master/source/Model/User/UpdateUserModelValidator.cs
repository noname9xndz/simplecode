namespace Architecture.Model
{
    public sealed class UpdateUserModelValidator : UserModelValidator
    {
        public UpdateUserModelValidator()
        {
            Id();
            Forename();
            Surname();
            Email();
        }
    }
}
