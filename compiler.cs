using System;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Vazilin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] commands = File.ReadAllText("script.vzln").Split('\n');
            int screenheight = Screen.PrimaryScreen.Bounds.Height;
            int screenwidth = Screen.PrimaryScreen.Bounds.Width;
            string lastInputResult = "virusin.string";
            string lastGEThttpResult = "virusin.string";
            foreach (string c in commands)
            {
                //Плэйсходеры и переносы
                string s = c.ToLower().Replace("\\n","\n");
                s = s.Replace("--lastinputed--", lastInputResult);
                s = s.Replace("--httpresult--", lastGEThttpResult);
                s = s.Replace("--screen.height--", screenheight + "");
                s = s.Replace("--screen.width--", screenwidth + "");
                s = s.Replace("--gettitle--", Console.Title);
                //Команды
                if (s.StartsWith("timer ")) { int ms = Convert.ToInt32(s.Substring(6)); Thread.Sleep(ms); }
                if (s.StartsWith("print ")) { string toprnt = s.Substring(6); Console.Write(toprnt); Console.CursorLeft = toprnt.Length-1; }
                if (s.StartsWith("printl ")) { string toprnt = s.Substring(7); Console.WriteLine(toprnt); }
                if (s.StartsWith("clear")) { Console.Clear(); }
                if (s.StartsWith("read-line")) { lastInputResult = Console.ReadLine(); }
                if (s.StartsWith("get-http-result ")) {string http = s.Substring(16); lastGEThttpResult = new HttpClient().GetStringAsync(@http).Result; }
                if (s.StartsWith("exe-open ")) { string proc = s.Substring(8); Process.Start(proc); }
                if (s.StartsWith("url-open ")) { string url = s.Substring(8); Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Internet Explorer\\iexplore.exe", url); }
                if (s.StartsWith("title ")) { string tlt = s.Substring(6); Console.Title = tlt; }
                if (s.StartsWith("version")) { Console.WriteLine("Virusin: 1.0.0.0A"); }
                //GDI
                if (s.StartsWith("gdi.blur")) { while (true) { GDI.blur(); } }
                if (s.StartsWith("gdi.inversion")) {
                    while (true) { GDI.inversion(); } }
                if (s.StartsWith("gdi.screenfun")) {
                    while (true) { GDI.screenoff(); } }
                if (s.StartsWith("gdi.fun")) {
                    while (true) { GDI.fun(); } }
                //app commands
                if (s.StartsWith("app.hide")) { GDI.ShowWindow(GDI.GetConsoleWindow(), GDI.SW_HIDE); }
                if (s.StartsWith("app.show")) { GDI.ShowWindow(GDI.GetConsoleWindow(), GDI.SW_SHOW); }
                if (s.StartsWith("app.exit")) { Application.Exit(); }
            }
        }
    }
}
