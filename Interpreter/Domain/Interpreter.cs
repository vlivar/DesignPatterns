namespace Domain;

public class Interpreter
{
    public static void Start()
    {
        // ( 13 + 4 ) - ( 12 + 1 )
        string input = "(20 + 8)-(44 + 4)";
        var tokens = Lexer.Lex(input);
        Console.WriteLine(string.Join("\t", tokens));

        var parsed = Parser.Parse(tokens);
        Console.WriteLine($"{input} = {parsed.Value}");
    }
}
