using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Core
{
    public delegate void DebugLog(String log);

    public class Logger
    {
        public Logger()
        {
        }

        public void Debug(String message)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + " " + message);
        }
    }
}
