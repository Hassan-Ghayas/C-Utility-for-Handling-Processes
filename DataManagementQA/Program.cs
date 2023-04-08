using System.Diagnostics;

namespace DataManagementQA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Please Provide All three Arguments");
                return;
            }

            //Getting Values From Arguments
            string processName = args[0];
            int maxLifeTime = int.Parse(args[1]);
            int frequency = int.Parse(args[2]);

            Console.WriteLine($"Monitoring {processName} every {frequency} minute(s)");

            Timer timer = new Timer(CheckProcesses, (processName, maxLifeTime), TimeSpan.Zero, TimeSpan.FromMinutes(frequency));

            while (Console.ReadKey().KeyChar.ToString().ToLower() != "q")
            {
                // Do nothing
            }

            timer.Dispose();
            Console.WriteLine("\nExiting...");
        }

        public static void CheckProcesses(object state)
        {
            (string processName, int maxLifetime) = ((string, int))state;

            // Getting processes with the given name
            Process[] processes = Process.GetProcessesByName(processName);

            if(processes.Count() ==0)
                Console.WriteLine("\nProcess Details\nNo process runnig at the moment");

            // Check process lifetime
            foreach (Process process in processes)
            {
                Console.WriteLine("\nProcess Details");
                Console.WriteLine($"Name : {processName}\tID : {process.Id}\tStartTime : {process.StartTime}\tLifeTime : {maxLifetime} minutes");
                if (DateTime.Now - process.StartTime > TimeSpan.FromMinutes(maxLifetime))
                {
                    Console.WriteLine($"\n{processName} process with ID {process.Id} has exceeded it's maximum lifetime. Process Terminated");
                    process.Kill();
                }
            }
        }
    }
}