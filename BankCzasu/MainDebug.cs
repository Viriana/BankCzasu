using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankCzasu
{
    class MainDebug
    {
        private static MainDebug instance;
        private static Object locker = new Object();
        //private List<string> log = new List<string>();
        private bool logToFile = false;
        private DateTime logCreation;

        public static MainDebug Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new MainDebug();
                        }
                    }
                }
                return instance;
            }
        }

        private MainDebug()
        {
            logCreation = DateTime.Now;
        }

        public static string Log(string message, Object sender = null)
        {
            string result = "";
            result = "[" + DateTime.Now.ToLongTimeString() + "] ";
            result += "<" + ((sender != null) ? sender.GetType().ToString() : "Anonymous Object") + "> ";
            result += message;
            //Instance.log.Add(result);
            Console.WriteLine(result);
            if (Instance.logToFile)
            {
                string path = "logs";
                string fileName = "";
                fileName += Instance.logCreation.Day.ToString() + "-";
                fileName += Instance.logCreation.Month.ToString() + "-";
                fileName += Instance.logCreation.Year.ToString() + " at ";
                fileName += Instance.logCreation.Hour.ToString() + " ";
                fileName += Instance.logCreation.Minute.ToString() + " ";
                fileName += Instance.logCreation.Second.ToString() + ".log";
                
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                path = Path.Combine(path, fileName);
                
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(result);
                }
                
            }
            return result;
        }
    }
}
