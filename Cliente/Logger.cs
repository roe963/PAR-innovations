using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cliente
{
    class Logger
    {        
        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine($"{logMessage}");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
