using System;
using System.Threading;
using Quartz;
using Quartz.Impl;

namespace QuartzConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter() { Level = Common.Logging.LogLevel.Info };
                //  Grab Scheduler instance from the factory
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                //  and start it off
                scheduler.Start();
                //  some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(5));
                //  and last shutdown the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
                throw;
            }
        }
    }
}
