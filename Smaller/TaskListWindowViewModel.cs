using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smaller.Tasks;

namespace Smaller
{
    public class TaskListWindowViewModel
    {
        private static RunHistory Null(RunHistory history)
        {
            return history ?? new RunHistory {
                Identifier = string.Empty,
                Result = string.Empty,
                RunDate = DateTime.MaxValue
            };
        }

        public TaskListWindowViewModel()
        {
        }

        public static TaskListWindowViewModel From(SmallerTaskList tasks, RunHistoryList history)
        {
            var jobs = tasks.Tasks
                .GroupJoin(history, 
                    o => o.Identifier, i => i.Identifier, 
                    (o, i) => new {Task = o, History = i})
                .SelectMany(io => io.History.DefaultIfEmpty(),
                    (o,i) => new {Task = o.Task, History=i})
                .Select(p => new Job
                {
                    Id = p.Task.Identifier,
                    Type = p.Task.GetType().Name,
                    RunDate = p.History == null ? null : (DateTime?)p.History.RunDate,
                    ScheduledDate = p.Task.ScheduledDate,
                    Result = Null(p.History).Result
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
            public DateTime? RunDate { get; set; }
            public string Result { get; set; }
        }

        public Job[] Things { get; set; }
    }
}
