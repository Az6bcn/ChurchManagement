using System.Collections.Generic;
using Domain.Abstracts;

namespace Domain.ValueObjects
{
    public class Person : ValueObject
    {
        internal Person(int tenantId,
                        string name,
                        string surname,
                        string dayMonthBirth,
                        string phoneNumber,
                        string gender)
        {
            Name = name;
            Surname = surname;
            DateAndMonthOfBirth = dayMonthBirth;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        public static Person Create(int tenantId,
                                    string name,
                                    string surname,
                                    string dayMonthBirth,
                                    string gender,
                                    string phoneNumber)
            => new Person(tenantId, name, surname, dayMonthBirth, phoneNumber, gender);

        public int TenantId { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string DateAndMonthOfBirth { get; protected set; }
        public string Gender { get; protected set; }
        public string PhoneNumber { get; protected set; }

        public IEnumerable<string> Validate()
        {
            if (TenantId == 0 || TenantId < 0)
                yield return $"{nameof(TenantId)} is required to create a member";

            if (string.IsNullOrWhiteSpace(Name))
                yield return $"{nameof(Name)} is required to create a member";

            if (string.IsNullOrWhiteSpace(Surname))
                yield return $"{nameof(Surname)} is required to create a member";

            if (string.IsNullOrWhiteSpace(Gender))
                yield return $"{nameof(Gender)} is required to create a member";

            if (string.IsNullOrWhiteSpace(DateAndMonthOfBirth))
                yield return $"{nameof(DateAndMonthOfBirth)} is required to create a member";

            if (!string.IsNullOrWhiteSpace(PhoneNumber))
                yield return $"{nameof(PhoneNumber)} is required to create a member";
        }

        public string FullName => $"{Name} {Surname}";

        protected override bool Equals(ValueObject value1, ValueObject value2)
        {
            throw new System.NotImplementedException();
        }
    }
}