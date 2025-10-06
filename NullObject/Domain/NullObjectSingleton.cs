namespace NullObject.Domain.NullObjectSingleton;

public class NullObjectSingleton
{
    public static void Start()
    {
        var nl = ILog.Null;
    }
}

public interface ILog
{
    void Warn();

    public static ILog Null => NullLog.Instance;

    private sealed class NullLog : ILog
    {
        private NullLog() { }

        private static Lazy<NullLog> _instance = new(() => new NullLog());
        public static ILog Instance => _instance.Value;

        public void Warn() { }
    }
}