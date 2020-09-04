using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLII_Andreja_Kolesar
{
    class LogIntoFile
    {
        public string path { get; } = @"..\..\Log.txt";
        private object locker = new object();
        private static LogIntoFile log;
        private LogIntoFile() { }
        public static LogIntoFile getInstance()
        {
            if (log == null)
                log = new LogIntoFile();
            return log;
        }
        /// <summary>
        /// Print currrent action with current time into file
        /// </summary>
        /// <param name="content"></param>
        public void PrintActionIntoFile(string content)
        {
            lock (locker)
            {
                try
                {
                    string currentDate = DateTime.Now.ToShortDateString();
                    string currentTime = DateTime.Now.ToShortTimeString();
                    content = currentDate + " " + currentTime + " " + content;
                    //print to file
                    Thread.Sleep(2000);
                    StreamWriter str = new StreamWriter(path, true);
                    str.WriteLine(content);
                    str.Close();
                }
                catch (FileNotFoundException)
                {
                   
                }
            }
        }
    }
}
