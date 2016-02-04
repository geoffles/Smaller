using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smaller.Tasks
{
    [DataContract]
    public class RunHistory
    {
        [DataMember]
        public string Identifier { get; set; }
        [DataMember]
        public DateTime RunDate { get; set; }
        [DataMember]
        public string Result { get; set; }
    }
}
