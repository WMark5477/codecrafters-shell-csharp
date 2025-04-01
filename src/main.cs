using System.Net;
using System.Net.Sockets;

bool Running = false;
Running = true;

while (Running) {
   Console.Write("$ ");
   var command = Console.ReadLine();
   Console.WriteLine($"{command}: command not found");
}

