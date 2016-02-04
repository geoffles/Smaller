using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Smaller.Tasks;

namespace Smaller
{
    public class JobRunner
    {
        public IList<RunHistory> Run()
        {

            var sj = new System.Xml.Serialization.XmlSerializer(typeof(SmallerTaskList), new[] { typeof(SmallerTaskBase), typeof(MailTask) });
            var sh = new System.Xml.Serialization.XmlSerializer(typeof(RunHistoryList));

            SmallerTaskList jobs = null;
            RunHistoryList history = null;
            
            using (var stream = new FileStream("jobs.big", FileMode.Open))
            {
                jobs = (SmallerTaskList)sj.Deserialize(stream);
            }

            using (var stream = new FileStream("jobs.small", FileMode.Open))
            {
                history = (RunHistoryList)sh.Deserialize(stream);
            }

            var jobsToRun = jobs
                .Tasks
                .Where(p => p.ScheduledDate <= DateTime.Now)
                .Where(p => history.All(q => q.Identifier != p.Identifier));

            var parameters = jobs.Parameters.ToDictionary(p => p.Key, p => p.Value);

            var runJobs = jobsToRun.Select(p => p.Run(parameters)).ToList();
            history.AddRange(runJobs);

            using (var stream = new FileStream("jobs.small", FileMode.Create))
            {
                sh.Serialize(stream, history);
            }

            return runJobs;

        }
    }
}
