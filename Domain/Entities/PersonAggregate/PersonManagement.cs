using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities.PersonAggregate
{
    public class PersonManagement: IEntity, IAggregateRoot
    {
        public Person Person { get; set; }
        public Member Member { get; set; }
        public NewComer NewComer { get; set; }
        public Department Department { get; set; }
    }
}