using System.Net;
using System.Net.Sockets;

bool Running = false;
Running = true;

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
        default:
            Console.WriteLine($"{command}: command not found");
            break;
    }
}

