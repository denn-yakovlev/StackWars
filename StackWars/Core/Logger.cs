using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace StackWars.Core
{
    class Logger
    {
        private Logger() { }

        private TextWriter outputStream = Console.Out;

        private static Lazy<Logger> lazyInstance = new Lazy<Logger>(() => new Logger());

        public static Logger Instance => lazyInstance.Value;

        public void Log(string text) => outputStream.WriteLine($"[{DateTime.Now}] {text}");
    }
}
