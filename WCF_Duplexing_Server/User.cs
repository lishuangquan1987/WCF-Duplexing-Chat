using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService;
using System.ComponentModel;
using System.ServiceModel;

namespace WCF_双工_Server
{
    /// <summary>
    /// 封装客户端信息，用于服务端
    /// </summary>
   public class User:INotifyPropertyChanged
    {
        private IChatToClient client;

        public IChatToClient Client
        {
            get { return client; }
            set { client = value; OnPropertyChanged("Client"); }
        }
        private string key;

       /// <summary>
       /// 客户端的唯一标识
       /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; OnPropertyChanged("Key"); }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }
        private IContextChannel chanel;

        public IContextChannel Chanel
        {
            get { return chanel; }
            set { chanel = value; OnPropertyChanged("Chanel"); }
        }
    
       public event PropertyChangedEventHandler PropertyChanged;
       private void OnPropertyChanged(string name)
       {
           if (PropertyChanged != null)
               this.PropertyChanged(this, new PropertyChangedEventArgs(name));
       }
    }
}
