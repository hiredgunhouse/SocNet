using System;
using System.Collections.Generic;
using Autofac;

namespace SocNet.ConsoleClient
{
    class Program
    {
        private static readonly string _prompt = "> ";

        static void Main(string[] args)
        {
            var container = new Bootstrapper().Initialize();
            var socApp = container.Resolve<SocApp>();

            PrintUsage();

            var quit = false;
            while (!quit)
            {
                Console.Write(_prompt);
                var command = Console.ReadLine();

                switch (command)
                {
                    case "quit":
                    case "q":
                    case "exit":
                        quit = true;
                        break;

                    default:
                        var result = socApp.Execute(command);
                        if (result != null)
                            PrintResult(result);

                        break;
                }
            }
        }

        private static void PrintResult(IEnumerable<string> result)
        {
            foreach (var line in result)
                Console.WriteLine(line);
        }

        private static void PrintUsage()
        {
            Console.WriteLine(@"
Usage:

Commands always start with the user’s name.
There are four commands.

posting:
<user name> -> <message>

reading:
<user name>

following:
<user name> follows <another user>

wall:
<user name> wall

Type quit, q or exit to exit application.
");
        }
    }
}
