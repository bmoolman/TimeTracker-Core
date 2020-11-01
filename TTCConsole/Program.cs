
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using TimeTrackerLib.Models;
using TimeTrackerLib.Repositories;
using TimeTrackerLib.Utils;

namespace TTCConsole
{
    class Program
    {

        private static string _datasource = @"c:\temp\timetrack.db";
        private static List<TTProject> _projectList;
        static void CreateDb()
        {
            SQLiteDbSetup setup = new SQLiteDbSetup(_datasource);
            setup.CreateDb();
        }
        static void Main(string[] args)
        {

            //CreateDb();

            ReadProjectSettings();
            Console.WriteLine("Enter the id of the project to start logging time against it.");

            foreach (var item in _projectList)
            {
                Console.WriteLine($"[{item.ProjectId}]\t {item.ProjectName}");
            }
            int projectId = Convert.ToInt32(Console.ReadLine());
            TTProject ttProject = GetProjectInfoFromId(projectId);


            if (ttProject.ProjectId==0)
            {
                Console.WriteLine($"[{ttProject.ProjectId}] DOES NOT EXIST! Please enter a description of the new project");
                ttProject.ProjectName = Console.ReadLine();
            }

            Console.WriteLine("Press ESC to stop logging");

            TTEvent ttevent = new TTEvent()
            {
                Id = 0,
                ProjectId = ttProject.ProjectId,
                ProjectName = ttProject.ProjectName,
                StartUTC = DateTime.UtcNow
            };

            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(2000);
                    TimeSpan span1 = DateTime.UtcNow - ttevent.StartUTC;
                    int totalSeconds1 = (int)span1.TotalSeconds;
                    Console.WriteLine($"{ttProject.ProjectName}: running for {totalSeconds1} secs.");

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            ttevent.EndUTC = DateTime.UtcNow;
            ttevent.Comments = "Time writing concluded";

            TimeSpan span = ttevent.EndUTC - ttevent.StartUTC;
            int totalSeconds = (int)span.TotalSeconds;

            Console.WriteLine($"Time logged for {ttProject.ProjectName}: {totalSeconds} secs.");
            Console.WriteLine($"Start time UTC {ttevent.StartUTC}");
            Console.WriteLine($"End time UTC {ttevent.EndUTC}");

            var rep = new SQLiteRepository(_datasource);
            rep.SaveEvent(ttevent);

            //List<TTEvent> events = rep.GetProjectEvents(2);

            //foreach (var item in events)
            //{
            //    TimeSpan ts = item.EndUTC - item.StartUTC;

            //    Console.WriteLine($"{item.ProjectName}: { ts.TotalSeconds}");
            //}
        }

        private static TTProject GetProjectInfoFromId(int projectId)
        {
            return _projectList.Where(p => p.ProjectId == projectId).FirstOrDefault() ?? new TTProject { ProjectId = 0, ProjectName = "unnamed project" };                       
        }

        static void ReadProjectSettings()
        {            
            var jsonString = File.ReadAllText("ProjectInfo.json");
            _projectList = JsonSerializer.Deserialize<List<TTProject>>(jsonString);
        }
    }
}
