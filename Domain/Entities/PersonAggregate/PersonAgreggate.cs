using Domain.Entities.NewComerAggregate;
using Domain.Interfaces;

namespace Domain.Entities.PersonAggregate
{
    public class PersonAggregate: IEntity, IAggregateRoot
    {
        public Member Member { get; set; }
        public NewComer NewComer { get; set; }
    }
}