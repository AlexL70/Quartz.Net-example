using Quartz;
using System;

namespace QuartzConsoleApp
{
    class HelloJob : IJob
    {
        private string _jobName;
        private IJobExecutionContext _context;
        private static object _lockObject = new object();

        protected string JobName
        {
            get
            {
                if (string.IsNullOrEmpty(_jobName))
                {
                    JobKey key = _context.JobDetail.Key;
                    JobDataMap map = _context.JobDetail.JobDataMap;
                    _jobName = map.GetString("JobName");
                    if (String.IsNullOrEmpty(_jobName))
                    {
                        _jobName = "HelloJob";
                    }
                }
                return _jobName;
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            if (_context == null)
            {
                _context = context;
            }
            lock (_lockObject)
            {
                Console.WriteLine($"Greetings from {JobName}!");
            }
        }
    }
}
