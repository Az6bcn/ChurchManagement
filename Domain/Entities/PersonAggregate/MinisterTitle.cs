using Domain.Abstracts;

namespace Domain.Entities.MemberAggregate
{
    public class MinisterTitle: IEntity
    {
        private MinisterTitle()
        {
        }

        internal MinisterTitle(string name): this()
        {
            Name = name;
        }

        internal MinisterTitle(int id, string name): this()
        {
            MinisterTitleId = id;
            Name = name;
        }

        public int MinisterTitleId { get; private set; }
        public string Name { get; private set; }


        public static MinisterTitle Create(string name) => new(name);
        public static MinisterTitle Create(int id, string name) => new(id, name);
    }
}