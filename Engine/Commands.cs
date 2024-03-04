using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    internal static class Commands
    {
        private static Dictionary<string, Action> _commandFunctions = new Dictionary<string, Action>();
        private static Dictionary<string, string> _commandDescriptions = new Dictionary<string, string>();
        internal static Dictionary<string, Action> CommandFunctions { get => _commandFunctions; set => _commandFunctions = value; }
        internal static Dictionary<string, string> CommandDescriptions { get => _commandDescriptions; set => _commandDescriptions = value; }

        static Commands() 
        {
            new BasicCommands();
        }

        public static void CreateNewCommand(string commandName, string description, Action action)
        {
            CommandFunctions.Add(commandName, action);
            CommandDescriptions.Add(commandName, description);
        }

        public static void ExecuteCommand(string commandName)
        {
            try
            {
                if (!CommandFunctions.ContainsKey(commandName))
                    throw new KeyNotFoundException(commandName);
                CommandFunctions.GetValueOrDefault(commandName).Invoke();
                return;
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Exception of type {nameof(KeyNotFoundException)}: {commandName} not found." +
                    $" Here is a list of available commands:");
            }
            CommandFunctions.GetValueOrDefault("/help").Invoke();
        }
    }
}