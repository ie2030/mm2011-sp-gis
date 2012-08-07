using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Collections;



namespace AlgorithmComponent
{
  
 
   [DataContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
   public class ReadingFromConfigFault 
    {
            private string operation;
            private string problem_message;
            private string problem_HelpLink;
            private string wrongName;

            [DataMember]
            public string Operation
            {
                get { return operation; }
                set { operation = value; }
            }

            [DataMember]
            public string Problem_message
            {
                get { return problem_message; }
                set { problem_message = value; }
            }

            [DataMember]
            public string Problem_HelpLink
            {
                get { return problem_HelpLink; }
                set { problem_HelpLink = value; }
            }

            [DataMember]
            public string WrongName
            { 
                get { return wrongName; } 
                set { wrongName = value; }
            }

            [DataMember]
            public List<DBComponent.Node> RightPathes { get; set; }
    }
}
