using DotNetCore.Domain;
using System.Collections.Generic;

namespace Architecture.Domain
{
    public sealed class Name : ValueObject
    {
        public Name(string forename, string surname)
        {
            Forename = forename;
            Surname = surname;
        }

        public string Forename { get; }

        public string Surname { get; }

        protected override IEnumerable<object> Equals()
        {
            yield return Forename;
            yield return Surname;
        }
    }
}
