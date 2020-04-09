using System;
using System.Collections.Generic;
using System.IO.Ports;
using CommandLine;
using static Z80SerialMonitor.ConsoleExtension;

namespace Z80SerialMonitor
{
    class Program
    {
        static Options options;

        static void HandleParseError(IEnumerable<Error> errs)
        {
            Environment.Exit(-1);
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed((opts) => { options = opts; })
                .WithNotParsed(HandleParseError);

            try
            {
                bool stop = false;
                using(var serialPort = new SerialPort(options.Port, options.Speed.Value))
                {
                    serialPort.ReadTimeout = 1500;
                    serialPort.WriteTimeout = 1500;
                    serialPort.Open();
                    while (!stop)
                    {
                        try
                        {
                            string message = serialPort.ReadLine();
                            writeMessage(message);
                        }
                        catch (TimeoutException) { }
                    }
                    serialPort.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            void writeMessage(string message)
            {
                if(!options.Colors)
                {
                    Console.WriteLine(message);
                }
                else
                {   
                    WriteColor(message.Substring(0, 17), ConsoleColor.Yellow);
                    WriteColor(" (");
                    WriteColor(message.Substring(19, 5), ConsoleColor.Yellow);
                    WriteColor(")    ");
                    WriteColor(message.Substring(29, 8), ConsoleColor.Magenta);
                    WriteColor(" (");
                    WriteColor(message.Substring(39, 2), ConsoleColor.Magenta);
                    WriteColor(")    ");
                    WriteColor(message.Substring(46, 2), ConsoleColor.DarkGreen, ConsoleColor.White);
                    WriteColor("|");
                    WriteColor(message.Substring(49, 4), ConsoleColor.Black, ConsoleColor.White);
                    WriteColor("|");
                    WriteColor(message.Substring(54, 4), ConsoleColor.Blue, ConsoleColor.White);
                    WriteColor("|");
                    WriteColor(message.Substring(59, 2), ConsoleColor.White, ConsoleColor.DarkGreen);
                    WriteColor("|");
                    WriteColor(message.Substring(62, 2), ConsoleColor.White, ConsoleColor.DarkRed);
                    WriteColor("|");
                    WriteLineColor(message.Substring(65, 4), ConsoleColor.Magenta, ConsoleColor.White);
                }
            }
        }
    }
}
