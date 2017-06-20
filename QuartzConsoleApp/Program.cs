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
                //  define the job and tie it to our HelloJob class
                IJobDetail firstJob = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .UsingJobData("JobName", "first hello job")
                    .Build();
                //  Trigger the job to run now and then repeat every 5 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(3).RepeatForever())
                    .Build();
                //  tell Quartz to schedule the job using the trigger
                scheduler.ScheduleJob(firstJob, trigger);
                //  some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(30));
                //  and last shutdown the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
                throw;
            }

            Console.WriteLine("Press any key to close the application.");
            Console.ReadKey();
        }
    }
}
