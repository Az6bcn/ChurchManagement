using System;
using System.Collections.Generic;
using Domain.Abstracts;

namespace Domain.Entities
{
    public class NewComer: Entity
    {
        private NewComer(): base()
        {
            
        }
        internal NewComer(
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            DateTime dateAttended,
            int serviceTypeId,
            Tenant tenant) 
        {
            Tenant = tenant;
            TenantId = tenant.TenantId;
            Name = name;
            Surname = surname;
            DateAndMonthOfBirth = dayMonthBirth;
            CreatedAt = DateTime.UtcNow;

            PhoneNumber = phoneNumber;
        }

        public int NewComerId { get; private set; }
        public int TenantId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string DateAndMonthOfBirth { get; private set; }
        public string Gender { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateAttended { get; private set; }
        public int ServiceTypeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        
        public Tenant Tenant { get; private set; }
        public ServiceType ServiceType { get; private set; }

        public static NewComer Create(
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            DateTime dateAttended,
            int serviceTypeId,
            Tenant tenant) => new NewComer(name, surname, dayMonthBirth, phoneNumber, dateAttended, serviceTypeId, tenant);
        
        public IEnumerable<string> Validate()
        {
            if (Tenant is null)
                yield return $"{nameof(Tenant)} is required to create a new comer";
            
            if (string.IsNullOrWhiteSpace(Name))
                yield return $"{nameof(Name)} is required to create a new comer";
            
            if (string.IsNullOrWhiteSpace(Surname))
                yield return $"{nameof(Surname)} is required to create a new comer";
            
            if (string.IsNullOrWhiteSpace(Gender))
                yield return $"{nameof(Surname)} is required to create a new comer";
            
            if (string.IsNullOrWhiteSpace(DateAndMonthOfBirth))
                yield return $"{nameof(DateAndMonthOfBirth)} is required to create a new comer";
            
            if ( DateAttended == default(DateTime))
                yield return $"{nameof(DateAttended)} is required to create a new comer";
        }
    }
    
}