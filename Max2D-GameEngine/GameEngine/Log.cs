using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Max2D_GameEngine.GameEngine
{
    public class Log
    {
        public static void Normal(string logMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[MESSAGE] - {logMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string logMessage)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[INFO] - {logMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string logMessage)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARNING] - {logMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string logMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] - {logMessage}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

}
