using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCFService
{
    [Serializable]
    [DataContract(IsReference=true)]
   public class MyImage
    {
        [DataMember]
        public string ImageName
        {
            get;
            set;
        }
        [DataMember]
        public byte[] Data
        {
            get;
            set;
        }
    }
}
