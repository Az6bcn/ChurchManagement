using System;

namespace Domain.Entities
{
    public class Minister: Member
    {
        private Minister()
        {
            
        }
        
        internal Minister(int tenantId,
            string name,
            string surname,
            string dayMonthBirth,
            string phoneNumber,
            int ministerTitleId,
            int serviceTypeId) : base()
        {
            base.Create(tenantId, name, surname, dayMonthBirth, isWorker: false, phoneNumber);
            MinisterTitleId =  ministerTitleId;
        }

        public int MinisterId { get; private set; }
        public int MinisterTitleId { get; private set; }
        public MinisterTitle MinisterTitle { get; private set; }



        public string Title => MinisterTitle.Name;


    }
}