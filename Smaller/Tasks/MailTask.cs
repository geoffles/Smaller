using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smaller.Tasks
{
    [DataContract]
    public class MailTask : SmallerTaskBase
    {
        [DataMember]
        public string To { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        protected override void OnRun(IDictionary<string, string> parameters)
        {
            var processedBody = Body;
            parameters.Keys.ToList().ForEach(key => processedBody = processedBody.Replace(string.Concat("%", key, "%"), parameters[key]));

            Process.Start(String.Format("mailto:{0}?subject={1}&body={2}", To, Subject, processedBody.Replace(" ", "%20").Replace("\r\n", "%0d%0a")));
        }
    }
}
