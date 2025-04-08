public static class Builtins
{
    public const string Exit = "exit";
    public const string Echo = "echo";
    public const string Type = "type";
    public const string PWD = "pwd";

    public static readonly string[] BuiltinCommands =
    {
        Exit,
        Echo,
        Type,
        PWD
    };

    public static void ExitCommand(string[] parameters)
    {
        if (parameters.Length == 0)
            Environment.Exit(0);

        else if (parameters.Length == 1 && parameters[0] == "0")
            Environment.Exit(0);

        else
            Console.WriteLine("exit: invalid parameter");
    }
    
    public static void EchoCommand(string[] parameters)
    {
        Console.WriteLine(string.Join(" ", parameters));
    }

    public static void TypeCommand(string[] parameters)
    {
        if (parameters.Length == 0)
        {
            Console.WriteLine("type: missing parameter");
            return;
        }
        foreach (var command in parameters)
        {
            if (BuiltinCommands.Contains(command))
                Console.WriteLine($"{command} is a shell builtin");

            else if (Utils.FindCommandInPath(command) is string path)
                Console.WriteLine($"{command} is {path}");

            else
                Console.WriteLine($"{command}: not found");
        }
    }
    public static void PWDCommand()
    {
        Console.WriteLine(Environment.CurrentDirectory);
    }
}