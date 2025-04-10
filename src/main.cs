using System.Net;
using System.Net.Sockets;

while (true)
{
    Console.Write("$ ");

    var input = Utils.ParseInput(Console.ReadLine()!);
    if (input.Length == 0)
        continue;
    var command = input[0].ToLower();
    var parameters = input.Length > 1 ? input[1..] : Array.Empty<string>();

    if (!Builtins.BuiltinCommands.Contains(command) && Utils.FindCommandInPath(command) is string path)
    {
        Utils.RunProcess(path, parameters);
        continue;
    }

    switch (command)
    {
        case Builtins.Exit:
            Builtins.ExitCommand(parameters); break;

        case Builtins.Echo:
            Builtins.EchoCommand(parameters); break;
            
        case Builtins.Type:
            Builtins.TypeCommand(parameters); break;

        case Builtins.PWD:
            Builtins.PWDCommand(); break;

        case Builtins.CD:
            Builtins.CDCommand(parameters); break;
        default:
            Console.WriteLine($"{command}: command not found"); break;
    }
}