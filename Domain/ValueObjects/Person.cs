namespace Domain.Entities.MemberAggregate
{
    public class Person
    {
        public int TenantId { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string DateAndMonthOfBirth { get; protected set; }
        public string Gender { get; protected set; }
        public string PhoneNumber { get; protected set; }
    }
}