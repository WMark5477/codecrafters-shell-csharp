using System.Net;
using System.Net.Sockets;

bool Running = false;
Running = true;

string[] builtins = { "exit", "echo", "type" };

while (Running) {
    Console.Write("$ ");

    var input = Console.ReadLine().Split(" ");
    var command = input[0];
    var parameters = input[1..];


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

