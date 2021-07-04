using System;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.PersonAggregate
{
    public class NewComer: IEntity
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
            // TenantId = tenant.TenantId;
            // Name = name;
            // Surname = surname;
            // DateAndMonthOfBirth = dayMonthBirth;
            CreatedAt = DateTime.UtcNow;

            //PhoneNumber = phoneNumber;
        }

        public int NewComerId { get; private set; }
        public DateTime DateAttended { get; private set; }
        public int ServiceTypeId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? Deleted { get; private set; }
        public int TenantId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string DateMonthOfBirth { get; private set; }
        public string Gender { get; private set; }
        public string PhoneNumber { get; private set; }
        
        
        public Person Person { get; private set; }
        public ServiceType ServiceType { get; private set; }
        public Tenant Tenant { get; set; }

        public static NewComer Create(
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            DateTime dateAttended,
            int serviceTypeId,
            Tenant tenant) => new NewComer(name, surname, dayMonthBirth, phoneNumber, dateAttended, serviceTypeId, tenant);
     
    }
    
}