using System.Collections;
using System.Collections.ObjectModel;

namespace Composite.NeutralNetworks;

public class NeutralNetworks
{
    public static void Start()
    {
        var n1 = new Neuron();
        var n2 = new Neuron();
        n1.Connect(n2);

        var l1 = new NeuronLayer();
        var l2 = new NeuronLayer();

        n1.Connect(l2);
        l1.Connect(l2);
    }
}

public static class NeuronExtensionsMethods
{
    public static void Connect(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
    {
        if (ReferenceEquals(self, other)) return;

        foreach (Neuron from in self)
        {
            foreach (Neuron to in other)
            {
                from.Out.Add(to);
                to.In.Add(from);
            }
        }
    }
}

public class Neuron : IEnumerable<Neuron>
{
    public float value;
    public List<Neuron> In = new(), Out = new();

    public IEnumerator<Neuron> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class NeuronLayer : Collection<Neuron>
{

}

public class NeuronRing : List<Neuron>
{

}