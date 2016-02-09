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
        public static SmallerTaskList GetAllTasks()
        {
            var sj = new System.Xml.Serialization.XmlSerializer(typeof(SmallerTaskList), new[] { typeof(SmallerTaskBase), typeof(MailTask) });
            using (var stream = new FileStream("jobs.big", FileMode.Open))
            {
                return (SmallerTaskList)sj.Deserialize(stream);
            }
        }

        public static RunHistoryList GetAllHistory()
        {
            var sh = new System.Xml.Serialization.XmlSerializer(typeof(RunHistoryList));
            using (var stream = new FileStream("jobs.small", FileMode.Open))
            {
                return (RunHistoryList)sh.Deserialize(stream);
            }

        }

        private static void SaveAllHistory(RunHistoryList history)
        {
            var sh = new System.Xml.Serialization.XmlSerializer(typeof(RunHistoryList));

            using (var stream = new FileStream("jobs.small", FileMode.Create))
            {
                sh.Serialize(stream, history);
            }
        }

        public IList<RunHistory> Run()
        {
            SmallerTaskList jobs = GetAllTasks();
            RunHistoryList history = GetAllHistory();
            
            var jobsToRun = jobs
                .Tasks
                .Where(p => p.ScheduledDate <= DateTime.Now)
                .Where(p => history.All(q => q.Identifier != p.Identifier));

            var parameters = jobs.Parameters.ToDictionary(p => p.Key, p => p.Value);

            var runJobs = jobsToRun.Select(p => p.Run(parameters)).ToList();
            history.AddRange(runJobs);

            SaveAllHistory(history);

            return runJobs;

        }


    }
}
