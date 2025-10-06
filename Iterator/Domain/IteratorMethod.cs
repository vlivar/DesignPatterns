namespace Domain;

public class IteratorMethod
{
    public static void Start()
    {
        //  1       in-order: 213
        // / \
        //2   3
        var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
        Console.WriteLine();

        var tree = new BinaryTree<int>(root);
        Console.WriteLine(string.Join(", ", tree.InOrder.Select(x => x.Value)));

        foreach (var node in tree)
            Console.Write($"{node.Value} ");
    }
}

public class Node<T>
{
    public T Value;
    public Node<T> Left, Right;
    public Node<T> Parent;

    public Node(T value)
    {
        Value = value;
    }

    public Node(T value, Node<T> left, Node<T> right)
    {
        Value = value;
        Left = left;
        Right = right;

        left.Parent = right.Parent = this;
    }
}

public class InOrderIterator<T>
{
    private readonly Node<T> _root;
    private bool _yieldedStart;

    public Node<T> Current { get; set; }

    public InOrderIterator(Node<T> root)
    {
        _root = root;
        Current = root;

        while (Current.Left != null)
            Current = Current.Left;
    }

    public bool MoveNext()
    {
        if (!_yieldedStart)
        {
            _yieldedStart = true;
            return true;
        }

        if (Current.Right != null)
        {
            Current = Current.Right;
            while (Current.Left != null)
                Current = Current.Left;

            return true;
        }
        else
        {
            var p = Current.Parent;
            while (p != null && Current == p.Right)
            {
                Current = p;
                p = p.Parent;
            }

            Current = p;

            return Current != null;
        }
    }

    public void Reset()
    {
        Current = _root;
        _yieldedStart = false;
    }
}

public class BinaryTree<T>
{
    private Node<T> _root;

    public BinaryTree(Node<T> root)
    {
        _root = root;
    }

    public IEnumerable<Node<T>> InOrder
    {
        get
        {
            foreach (var node in Traverse(_root))
                yield return node;

            IEnumerable<Node<T>> Traverse(Node<T> current)
            {
                if (current.Left != null)
                {
                    foreach (var left in Traverse(current.Left))
                        yield return left;
                }

                yield return current;

                if (current.Right != null)
                {
                    foreach (var right in Traverse(current.Right))
                        yield return right;
                }
            }
        }
    }

    public InOrderIterator<T> GetEnumerator()
    {
        return new InOrderIterator<T>(_root);
    }
}