using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smaller.Tasks
{
    [DataContract]
    public class SmallerTaskList
    {
        [DataMember]
        public List<SmallerTaskBase> Tasks { get; set; }

        [DataMember]
        public List<Parameter> Parameters { get; set; } 
    }
}
