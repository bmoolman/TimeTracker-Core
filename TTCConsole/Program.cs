
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
        private static List<TTProjectSummary> _projectList;

        private static string updateText;
        static void CreateDb()
        {
            SQLiteDbSetup setup = new SQLiteDbSetup(_datasource);
            setup.CreateDb();
        }
        static void Main(string[] args)
        {

            //CreateDb(); return;

            var rep = new SQLiteRepository(_datasource);

            //ReadProjectSettings(); //Loads json file

            _projectList = rep.GetProjects();

            Console.WriteLine("Enter the id of the project to start logging time against it.");

            foreach (var item in _projectList)
            {
                Console.WriteLine($"[{item.ProjectId}]\t {item.ProjectName}\t\t [{item.SumLoggedMinutes} mins.]");
            }
            int projectId = Convert.ToInt32(Console.ReadLine());
            TTProject ttProject = GetProjectInfoFromId(projectId);


            if (ttProject.ProjectId == 0)
            {
                Console.WriteLine($"[{ttProject.ProjectId}] DOES NOT EXIST! Enter the name of the new project");
                ttProject.ProjectName = Console.ReadLine();
                Console.WriteLine($"[{ttProject.ProjectName}] - Enter a description of the new project");
                ttProject.ProjectDescription = Console.ReadLine();
                ttProject.ProjectId = rep.CreateNewProject(ttProject);
                Console.WriteLine($"[{ttProject.ProjectName}] - Create successfully\n");
            }


            Console.WriteLine($"What are you currently working on for {ttProject.ProjectName}");
            string comments = Console.ReadLine();

            Console.WriteLine("Press ESC to stop logging");

            TTEvent ttevent = new TTEvent()
            {
                Id = 0,
                ProjectId = ttProject.ProjectId,
                StartUTC = DateTime.UtcNow,
                Comments = comments
            };

            Timer timer = new Timer(Callback);
            timer.Change(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(60));

            updateText = $"{ttProject.ProjectId}-{ttProject.ProjectName}";

            do
            {
                while (!Console.KeyAvailable)
                {

                    TimeSpan span1 = DateTime.UtcNow - ttevent.StartUTC;
                    int totalSeconds1 = (int)span1.TotalSeconds;
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            ttevent.EndUTC = DateTime.UtcNow;

            TimeSpan span = ttevent.EndUTC - ttevent.StartUTC;
            int totalSeconds = (int)span.TotalSeconds;

            ttevent.LoggedMinutes = totalSeconds / 60 == 0 ? 1: totalSeconds / 60;

            Console.WriteLine($"Time logged for {ttProject.ProjectName}: {totalSeconds} secs.");
            Console.WriteLine($"Start time UTC {ttevent.StartUTC}");
            Console.WriteLine($"End time UTC {ttevent.EndUTC}");

            Console.WriteLine("Do you want to overwrite the time logged? [y/n]");

            string OverrideText = Console.ReadLine();
            if (OverrideText.ToLower()=="y")
            {
                Console.WriteLine("Enter the amount of time spent on this activity in minutes");
                int overrideMins = Convert.ToInt32(Console.ReadLine());
                ttevent.IsCustom = true;
                ttevent.LoggedMinutes = overrideMins;
            }

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
            _projectList = JsonSerializer.Deserialize<List<TTProjectSummary>>(jsonString);
        }

        private static void Callback(object state)
        {
            Console.WriteLine(updateText);
        }
    }
}
