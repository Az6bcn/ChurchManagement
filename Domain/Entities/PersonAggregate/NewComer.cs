using System;
using System.Collections.Generic;
using Domain.Entities.Helpers;
using Domain.Entities.TenantAggregate;
using Domain.Interfaces;
using Domain.Validators;
using Domain.ValueObjects;
using Shared.Enums;

namespace Domain.Entities.PersonAggregate
{
    public class NewComer : IEntity
    {
        private NewComer() : base()
        {
        }

        internal NewComer(Person person,
                          DateTime dateAttended,
                          ServiceEnum serviceTypeEnum,
                          Tenant tenant)
        {
            if (!Validate(dateAttended, out IDictionary<string, object> error))
                throw new DomainValidationException("Failed domain validation", error);
            
            var serviceTypeEnumValue = GetServiceTypeEnumValue(serviceTypeEnum);
            ServiceType = ServiceType.Create(serviceTypeEnumValue.Id, serviceTypeEnumValue.Value);

            TenantId = tenant.TenantId;
            Name = person.Name;
            Surname = person.Surname;
            Gender = person.Gender;
            DateMonthOfBirth = person.DateAndMonthOfBirth;
            DateAttended = dateAttended;
            ServiceTypeId = serviceTypeEnumValue.Id;
            PhoneNumber = person.PhoneNumber;
            CreatedAt = DateTime.UtcNow;
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

        public static NewComer Create(Person person,
                                      DateTime dateAttended,
                                      ServiceEnum serviceTypeEnum,
                                      Tenant tenant) => new(person, dateAttended, serviceTypeEnum, tenant);

        public void Update(Person person,
                                  DateTime dateAttended,
                                  ServiceEnum serviceEnumType)
        {
            if (!Validate(dateAttended, out IDictionary<string, object> error))
                throw new DomainValidationException("Failed domain validation", error);
            
            TenantId = person.TenantId;
            Name = person.Name;
            Surname = person.Surname;
            DateMonthOfBirth = person.DateAndMonthOfBirth;
            Gender = person.Gender;
            PhoneNumber = person.PhoneNumber;
            DateAttended = dateAttended;
            ServiceTypeId = (int) serviceEnumType;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            Deleted = DateTime.UtcNow;
        }

        private EnumValue GetServiceTypeEnumValue(ServiceEnum serviceTypeEnum)
            => EnumService<ServiceEnum>.GetValue(serviceTypeEnum);

        private bool Validate(DateTime dateAttended,
                              out IDictionary<string, object> error)
        {
            error = new Dictionary<string, object>();
            
            if (dateAttended == new DateTime())
            {
                error.Add(nameof(dateAttended), "Invalid date value for date attended");

                return false;
            }

            return true;
        }
    }
}