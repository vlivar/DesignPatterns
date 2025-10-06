namespace Facade;

public class FacadeExample
{
    public static void Start()
    {
        var con = new ConsoleFacade();
        con.Write();
    }
}

public interface IDoThings
{
    void Open();
    void DoThing();
    void Close();
}
public interface ITextureMangar : IDoThings { }
public interface IViewport : IDoThings { }

public class ConsoleFacade
{
    private ITextureMangar _textureMangar;
    private IViewport _viewport;

    public ConsoleFacade()
    {
    }

    public void Write()
    {
        if (_textureMangar is null || _viewport is null)
        {
            Console.WriteLine("textureMangar or viewport is null");
            return;
        }

        _textureMangar.Open();
        _textureMangar.DoThing();

        _viewport.Open();
        _viewport.DoThing();

        _viewport.Close();
        _textureMangar.Close();

        Console.WriteLine("Work...");
    }
}