using System.Dynamic;
using ImpromptuInterface;

namespace Domain.DynamicNullObject;

public class DynamicNullObject
{
    public static void Start()
    {
        var log = Null<ILog>.Instance;
        log.Info("test");
        var ba = new BankAccount(log);
        ba.Deposit(100);
    }
}
public interface ILog
{
    void Info(string msg);
    void Warn(string msg);
}

public class ConsoleLog : ILog
{
    public void Info(string msg) => Console.WriteLine(msg);

    public void Warn(string msg) => Console.WriteLine($"Warning: {msg}");
}

public class BankAccount
{
    private ILog log;
    private int balance;

    public BankAccount(ILog log) => this.log = log;

    public void Deposit(int amount)
    {
        balance += amount;
        log.Info($"Deposited: {amount}, balance: {balance}");
    }
}

public class NullLog : ILog
{
    public void Info(string msg) { }

    public void Warn(string msg) { }
}

public class Null<TInterface> : DynamicObject
    where TInterface : class
{
    public static TInterface Instance => new Null<TInterface>().ActLike<TInterface>();

    public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    {
        result = Activator.CreateInstance(binder.ReturnType)!;
        return true;
    }

}