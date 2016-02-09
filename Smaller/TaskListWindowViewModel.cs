using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smaller.Tasks;

namespace Smaller
{
    public class TaskListWindowViewModel
    {
        private TaskListWindowViewModel()
        {
        }

        public static TaskListWindowViewModel From(SmallerTaskList tasks, RunHistoryList history)
        {
            var jobs = tasks.Tasks
                .Join(history, o => o.Identifier, i => i.Identifier, (o, i) => new {Task = o, History = i})
                .Select(p => new Job
                {
                    Id = p.Task.Identifier,
                    Type = p.Task.GetType().Name,
                    RunDate = p.History.RunDate,
                    ScheduledDate = p.Task.ScheduledDate,
                    Result = p.History.Result
                });

            return new TaskListWindowViewModel
            {
                Things = jobs.ToArray()
            };
        }

        public class Job
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public DateTime ScheduledDate { get; set; }
            public DateTime RunDate { get; set; }
            public string Result { get; set; }
        }

        public Job[] Things { get; set; }
    }
}
