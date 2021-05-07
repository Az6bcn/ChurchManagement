using System;

namespace Domain.Entities
{
    public class NewComer: Member
    {
        private NewComer(): base()
        {
            
        }
        internal NewComer(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            DateTime dateAttended,
            int serviceTypeId) : base()
        {
            base.Create(tenantId, name, surname, dayMonthBirth, isWorker: false, phoneNumber);
            DateAttended = dateAttended;
            ServiceTypeId = serviceTypeId;
        }

        public int NewComerId { get; private set; }
        public DateTime DateAttended { get; private set; }
        public int ServiceTypeId { get; private set; }
        public ServiceType ServiceType { get; private set; }

        public static NewComer Create(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            DateTime dateAttended,
            int serviceTypeId) => new NewComer(tenantId, name, surname, dayMonthBirth, phoneNumber, dateAttended, serviceTypeId);
    }
}