using System;

namespace Z80SerialMonitor
{
    public static class ConsoleExtension
    {
        public static void WriteColor(string val, ConsoleColor? foreground = null, ConsoleColor? background = null)
        {
            // Save current colors (hope they are the default ones)
            ConsoleColor currentForeground = Console.ForegroundColor;
            ConsoleColor currentBackground = Console.BackgroundColor;

            if(foreground.HasValue)
            {
                Console.ForegroundColor = foreground.Value;
            }
            if(background.HasValue)
            {
                Console.BackgroundColor = background.Value;
            }
            Console.Write(val);

            // Restore previoulsy saved colors
            Console.ForegroundColor = currentForeground;
            Console.BackgroundColor = currentBackground;
        }

        public static void WriteLineColor(string val, ConsoleColor? foreground = null, ConsoleColor? background = null)
        {
            WriteColor($"{val}\n", foreground, background);
        }
    }
}