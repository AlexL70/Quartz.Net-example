using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzConsoleApp
{
    class HelloJob : IJob
    {
        private string _jobName;
        private IJobExecutionContext _context;

        protected string JobName
        {
            get
            {
                if (String.IsNullOrEmpty(_jobName))
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
            Console.WriteLine($"Greetings from {JobName}!");
        }
    }
}
