using CommandLine;
using System;
using System.Linq;

namespace Z80SerialMonitor
{
    public class Options
    {
        // These values are taken from the options list of the Arduino IDE's serial monitor
        static readonly int[] okComSpeeds =
        {
            300,    1200,    2400,   4800,
            9600,   19200,   38400,  57600,
            74880,  115200,  230400, 250000,
            500000, 1000000, 2000000
        };

        // Change here the default serial port speed
        const int defaultComSpeed = 115200;

        // Of course, users entering "DonaldDuck" as the serial device will make the monitor go *BOOM* on serialPort.Open()...
        [Option('p', "port", Required = true, HelpText = "Serial port to open, e.g. COM4 or /dev/ttyUSB0.")]
        public string Port { get; set; }

        private int speed;
        // Behind the scenes, we can call Speed.Value without checking .HasValue
        [Option('s', "speed", Default = defaultComSpeed, Required = false, HelpText = "Serial port speed (in bauds).")]
        public int? Speed
        {
            get => speed;
            set
            {
                if(!value.HasValue || okComSpeeds.Contains(value.Value))
                {
                    speed = value ?? defaultComSpeed;
                }
                else
                {
                    throw new ArgumentException($"Serial port speed must be one of the following: {string.Join(", ", okComSpeeds)}");
                }
            }
        }

        [Option('c', "colors", Default = false, Required = false, HelpText = "Display using colors.")]
        public bool Colors { get; set; }
    }
}