using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WCFService
{
    /// <summary>
    /// 好友类
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
   public class MyUser:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
       private void OnPropertyChanged(string name)
       {
           if (PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs(name));
       }
        private string key;

        /// <summary>
        /// 好友唯一标识，服务端的标识为“-1”
        /// </summary>
        [DataMember]
        public string Key
        {
            get { return key; }
            set { key = value; OnPropertyChanged("Key"); }
        }
        private string userName;
       /// <summary>
       ///好友名称
       /// </summary>
        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }
    }
}
