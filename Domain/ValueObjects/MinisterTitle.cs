using Domain.Abstracts;

namespace Domain.ValueObjects;

public class MinisterTitle: ValueObject
{
    private MinisterTitle()
    {
    }

    internal MinisterTitle(int id, string name): this()
    {
        MinisterTitleId = id;
        Name = name;
    }

    public int MinisterTitleId { get; private set; }
    public string Name { get; private set; }
        
    public static MinisterTitle Create(int id, string name) => new(id, name);
    public static MinisterTitle Update(string name, int id) => new MinisterTitle(id, name);
        
    protected override bool Equals(ValueObject value1, ValueObject value2)
    {
        throw new System.NotImplementedException();
    }
}