using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Abstracts;

namespace Domain.ValueObjects;

public class Person : ValueObject
{
    public Person()
    {
    }

    internal Person(int tenantId,
                    string name,
                    string surname,
                    string dayMonthBirth,
                    string phoneNumber,
                    string gender)
    {
        TenantId = tenantId;
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

    [Key] public int TenantId { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string DateAndMonthOfBirth { get; private set; }
    public string Gender { get; private set; }
    public string PhoneNumber { get; private set; }

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

        if (string.IsNullOrWhiteSpace(PhoneNumber))
            yield return $"{nameof(PhoneNumber)} is required to create a member";

        if (!Regex.IsMatch(PhoneNumber, @"^\+(?:[0-9]●?){6,14}[0-9]$"))
            yield return $"{nameof(PhoneNumber)} {PhoneNumber} is not a valid phone number";
    }

    public string FullName => $"{Name} {Surname}";

    protected override bool Equals(ValueObject value1,
                                   ValueObject value2)
    {
        throw new System.NotImplementedException();
    }
}