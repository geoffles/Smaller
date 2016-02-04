using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smaller.Tasks
{
    [DataContract]
    [KnownType(typeof(MailTask))]
    public abstract class SmallerTaskBase
    {
        [DataMember]
        public DateTime ScheduledDate { get; set; }

        [DataMember]
        public string Identifier { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        public RunHistory Run(Dictionary<string, string> parameters)
        {
            try
            {
                OnRun(parameters);
            }
            catch (Exception ex)
            {
                return new RunHistory
                {
                    Identifier = this.Identifier,
                    Result = "Failed: " + ex.ToString(),
                    RunDate = DateTime.Now
                };
            }

            return new RunHistory
            {
                Identifier = this.Identifier,
                Result = "Success",
                RunDate = DateTime.Now
            };
            
        }

        protected abstract void OnRun(IDictionary<string, string> parameters );
    }
}
