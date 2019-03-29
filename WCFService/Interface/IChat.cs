using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCFService
{
  [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatToClient))]
  public interface IChatToServer
    {
      /// <summary>
      /// 客户端发布消息，根据key来判断向服务器还是客户端发送
      /// </summary>
      /// <param name="key"></param>
      /// <param name="msg"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessageToServer(string fromKey, string toKey, string msg);
        [OperationContract(IsOneWay = true)]
        void SendImageToServer(string fromKey, string toKey, MyImage image);
        [OperationContract(IsOneWay = true)]     
        void Login(string userName);
        //客户端请求获取好友列表
        [OperationContract(IsOneWay = true)]
        void GetFriendList();
    }
  public interface IChatToClient
  {
      [OperationContract(IsOneWay = true)]
      void SendMessageToClient(string fromKey, string toKey, string msg);
      [OperationContract(IsOneWay = true)]
      void SendImageToClient(string fromKey, string toKey, MyImage image);
      //服务端发送好友列表
      [OperationContract(IsOneWay = true)]
      void SendFriendList(List<MyUser> lstMyUser, string tip);
  }
}
