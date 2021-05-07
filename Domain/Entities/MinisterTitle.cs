using System.Collections.Generic;

namespace Domain.Entities
{
    public class MinisterTitle
    {
        private HashSet<Minister> _ministers;
        private MinisterTitle()
        {
            _ministers = new HashSet<Minister>();
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



        public IReadOnlyCollection<Minister> Ministers => _ministers;
        public static MinisterTitle Create(string name) => new(name);
        public static MinisterTitle Create(int id, string name) => new(id, name);
    }
}