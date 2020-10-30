
using System;
using System.Threading;
using TimeTrackerLib.Models;
using TimeTrackerLib.Repositories;
using TimeTrackerLib.Utils;

namespace TTCConsole
{
    class Program
    {

        private static string _datasource = @"c:\temp\timetrack.db";
        static void CreateDb()
        {
            SQLiteDbSetup setup = new SQLiteDbSetup(_datasource);
            setup.CreateDb();
        }
        static void Main(string[] args)
        {

            //CreateDb();

            Console.WriteLine("Enter the name of the project to start logging time against it.");
            string projectName = Console.ReadLine();
            Console.WriteLine("Press ESC to stop");

            TTEvent ttevent = new TTEvent()
            {
                Id = 0,
                ProjectId = 1,
                ProjectName = projectName,
                StartUTC = DateTime.UtcNow
            };

            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(2000);
                    TimeSpan span1 = DateTime.UtcNow - ttevent.StartUTC;
                    int totalSeconds1 = (int)span1.TotalSeconds;
                    Console.WriteLine($"{projectName}: running for {totalSeconds1} secs.");

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            ttevent.EndUTC = DateTime.UtcNow;
            ttevent.Comments = "Time writing concluded";

            TimeSpan span = ttevent.EndUTC - ttevent.StartUTC;
            int totalSeconds = (int)span.TotalSeconds;

            Console.WriteLine($"Time logged for {projectName}: {totalSeconds} secs.");
            Console.WriteLine($"Start time UTC {ttevent.StartUTC}");
            Console.WriteLine($"End time UTC {ttevent.EndUTC}");

            var rep = new SQLiteRepository(_datasource);
            rep.SaveEvent(ttevent);

        }
    }
}
