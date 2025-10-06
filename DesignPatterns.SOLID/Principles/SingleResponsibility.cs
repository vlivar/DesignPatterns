using System.Diagnostics;

namespace SOLID.Principles;

public class SingleResponsibility
{
    public static void Start()
    {
        var j = new Journal();
        j.AddEntry("First entry");
        j.AddEntry("Second entry");
        Console.WriteLine(j);

        var p = new Persistence<Journal>();
        var filename = @"C:\Users\ivar\Desktop\journal.txt";
        p.SaveToFiel(j, filename, true);
        Process.Start("notepad.exe", filename);
    }
}

public class Journal
{
    public readonly List<string> entries = new();
    private static int count = 0;

    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count;
    }

    public int RemoveEntry(int index)
    {
        entries.RemoveAt(index);
        return --count;
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }
}

public class Persistence<T>
{
    public void SaveToFiel(T obj, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, obj!.ToString());
    }
}