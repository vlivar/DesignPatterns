namespace SOLID.Principles;

public class DependencyInversion
{
    public enum Relationship
    {
        Parent, Child, Sinling
    }

    public class Person
    {
        public required string Name;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(Person p);
    }

    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> _relations = new();

        public void AddParentChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(Person p)
        {
            foreach (var r in _relations.Where(x =>
                x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
            {
                yield return r.Item3;
            }
        }
    }

    public class Research
    {
        public Research(IRelationshipBrowser browser, Person p)
        {
            foreach (var c in browser.FindAllChildrenOf(p))
                Console.WriteLine($"John has a child called {c.Name}");

        }
    }

    public static void Start()
    {
        var parent = new Person { Name = "John" };
        var child1 = new Person { Name = "Chris" };
        var child2 = new Person { Name = "Mary" };

        var relationships = new Relationships();
        relationships.AddParentChild(parent, child1);
        relationships.AddParentChild(parent, child2);

        _ = new Research(relationships, parent);
    }
}