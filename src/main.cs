using System.Net;
using System.Net.Sockets;

static bool IsExecutable(string filePath)
{
    string[] executables = { ".exe", ".bat", ".cmd" };
    return executables.Contains(Path.GetExtension(filePath).ToLower());
}

static string? FindCommandInPath(string command)
{
    var paths = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? Array.Empty<string>();

    foreach (var path in paths)
    {
        Console.WriteLine(path);
        string fullPath = Path.Combine(path, command);
        if (File.Exists(fullPath) && IsExecutable(fullPath));
            return fullPath;
    }

    return null;
}

bool Running = false;
Running = true;

string[] builtins = { "exit", "echo", "type" };

while (Running)
{
    Console.Write("$ ");

    var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (input == null || input.Length == 0) continue;

    var command = input[0];
    var parameters = input.Length > 1 ? input[1..] : Array.Empty<string>();
    
    switch (command)
    {
        case "exit":
            if (parameters.Length != 0 && parameters[0] == "0") 
                Running = false;
            else
                Console.WriteLine("exit: invalid parameter");
            break;

        case "echo":
            Console.WriteLine(string.Join(" ", parameters));
            break;

        case "type":
            if (parameters.Length != 0)
            {
                if (builtins.Contains(parameters[0]))
                    Console.WriteLine($"{parameters[0]} is a shell builtin");

                else if (FindCommandInPath(parameters[0]) is string path)
                    Console.WriteLine($"{parameters[0]} is {path}");

                else
                    Console.WriteLine($"{parameters[0]}: not found");
            }
            else
                Console.WriteLine("type: missing parameter");
            break;
        default:
            Console.WriteLine($"{command}: command not found");
            break;
    }
}
