using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService;

namespace WCF_双工_Client
{
   public class ChatToClient:IChatToClient
    {
       /// <summary>
       /// 定义委托
       /// </summary>
       /// <param name="msg"></param>
       public delegate void Dele_ReceiveMsg(string fromKey,string toKey,string msg);
       public delegate void Dele_ReceiveImage(string fromKey,string toKey,MyImage image);
       public delegate void Dele_ReceiveFriendList(List<MyUser> lstMyUser,string tip);
        /// <summary>
        /// 定义事件
        /// </summary>
       public event Dele_ReceiveMsg ReceiveMsgEvent;
       public event Dele_ReceiveImage ReceiveImageEvent;
       //好友列表更新事件
       public event Dele_ReceiveFriendList ReceiveFriendListEvent;
       #region~实现IChatToClient
       public void SendMessageToClient(string fromKey,string toKey,string msg)
        {
            if (ReceiveMsgEvent != null)
                ReceiveMsgEvent(fromKey,toKey,msg);
        }
       public void SendImageToClient(string fromKey, string toKey, MyImage image)
        {
            if (ReceiveImageEvent != null)
                ReceiveImageEvent(fromKey,toKey,image);
        }       
        public void SendFriendList(List<MyUser> lstMyUser, string tip)
        {
            if (ReceiveFriendListEvent != null)
                ReceiveFriendListEvent(lstMyUser, tip);
        }
       #endregion
    }
}
