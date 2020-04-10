using System;

namespace Z80SerialMonitor
{
    public static class ConsoleExtension
    {
        static ConsoleColor currentForeground;
        static ConsoleColor currentBackground;

        public static void SaveCurrentColors()
        {
            // Save current colors (hope they are the default ones)
            currentForeground = Console.ForegroundColor;
            currentBackground = Console.BackgroundColor;
        }

        public static void RestoreColors()
        {
            // Restore previoulsy saved colors
            Console.ForegroundColor = currentForeground;
            Console.BackgroundColor = currentBackground;
        }

        public static void WriteColor(string val, ConsoleColor? foreground = null, ConsoleColor? background = null)
        {
            if(foreground.HasValue)
            {
                Console.ForegroundColor = foreground.Value;
            }
            if(background.HasValue)
            {
                Console.BackgroundColor = background.Value;
            }
            Console.Write(val);
            RestoreColors();
        }

        public static void WriteLineColor(string val, ConsoleColor? foreground = null, ConsoleColor? background = null)
        {
            WriteColor(val, foreground, background);
            Console.Write("\n");
        }
    }
}