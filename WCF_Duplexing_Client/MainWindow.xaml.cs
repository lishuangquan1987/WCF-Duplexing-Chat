using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WCFService;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Configuration;
using System.Collections.ObjectModel;

namespace WCF_双工_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string UserName = "";
        public ObservableCollection<MyUser> LstUser = new ObservableCollection<MyUser>();
        public MainWindow()
        {
            Login login = new Login();
            if (login.ShowDialog() != true)
            {
                Application.Current.Shutdown(-1);
            }
            UserName = login.tb_UserName.Text;
            InitializeComponent();
            this.Title = "Client-" + UserName;

            //好友列表绑定
            this.listBox.ItemsSource = LstUser;
            this.listBox.DisplayMemberPath = "UserName";

            this.allRb.IsChecked = true;

            #region~loadingFrame
            Loading loadingpage = new Loading();
            loadingpage.lb_Msg.Content = "正在连接服务器，请稍后...";
            this.loadingFrame.Content = loadingpage;
            #endregion
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string ipAdress = ConfigurationManager.AppSettings["ipAdress"];
            new System.Threading.Thread(() =>
            {
                ChatToClient server = new ChatToClient();
                server.ReceiveMsgEvent += server_ReceiveMsgEvent;
                server.ReceiveImageEvent += server_ReceiveImageEvent;
                server.ReceiveFriendListEvent += GetOrUpdateFriendList;
                InstanceContext context = new InstanceContext(server);
                WSDualHttpBinding bingding = new WSDualHttpBinding();
                bingding.MaxReceivedMessageSize = Int32.MaxValue;
                bingding.MaxBufferPoolSize = Int32.MaxValue;
                bingding.OpenTimeout = new TimeSpan(0, 20, 0);
                bingding.SendTimeout = new TimeSpan(0, 20, 0);
                //必须加上，否则传输大文件有问题..
                bingding.ReaderQuotas.MaxDepth = int.MaxValue;
                bingding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                bingding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                bingding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                bingding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
               
                //安全性
                bingding.UseDefaultWebProxy = false;
                bingding.Security.Message.ClientCredentialType = MessageCredentialType.None;
                bingding.Security.Mode = WSDualHttpSecurityMode.None;

                factory = new DuplexChannelFactory<IChatToServer>(context, bingding, new EndpointAddress(ipAdress));
                foreach (OperationDescription op in factory.Endpoint.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = Int32.MaxValue;
                    }
                }
                try
                {
                    client = factory.CreateChannel();                    
                    client.Login(UserName);
                    client.GetFriendList();
                    UpdateText(DateTime.Now.ToString() + "  已经与服务器连接..", Colors.Gray, true);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.loadingFrame.Visibility = System.Windows.Visibility.Collapsed;
                        this.mainGrid.Visibility = System.Windows.Visibility.Visible;
                    }));
                }
                catch (Exception ee)
                {
                    UpdateText(DateTime.Now.ToString() + "  " + ee.Message + "\r\n请重启客户端重新来连接", Colors.Red, true);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.loadingFrame.Visibility = System.Windows.Visibility.Collapsed;
                        this.mainGrid.Visibility = System.Windows.Visibility.Visible;
                    }));
                }
            }).Start();

        }

        private void GetOrUpdateFriendList(List<MyUser> lstMyUser, string tip)
        {
            if (tip != null)
                this.UpdateText(string.Format("{0} {1}",DateTime.Now,tip), Colors.Gray, true);
            this.Dispatcher.Invoke(new Action(() => { LstUser.Clear(); lstMyUser.ForEach(x => LstUser.Add(x)); }));
        }
        IChatToServer client = null;
        DuplexChannelFactory<IChatToServer> factory;
        private void btn_SendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_preChat.Text == "")
                return;
            string msg = "";
            if (this.allRb.IsChecked == true)//群发消息,循环发送
            {
                var keyList = from i in LstUser select i.Key;
                //fromKey在服务端获取
                keyList.ToList().ForEach(x => client.SendMessageToServer(null,x,"[群发消息]"+this.tb_preChat.Text));
                msg = string.Format("我 {0} 群发了消息：{1}", DateTime.Now, this.tb_preChat.Text);
            }
            else//对单个客户端发送
            {
                if (this.listBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择好友来发送！");
                    return;
                }
                string key = (this.listBox.SelectedItem as MyUser).Key;
                client.SendMessageToServer(null,key, this.tb_preChat.Text);
                msg = string.Format("我 {0}：{1}", DateTime.Now, tb_preChat.Text);
            }           
            UpdateText(msg, Colors.Green, true);
            this.tb_preChat.Clear();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btn_SendMsg_Click(this, e);
            base.OnKeyDown(e);
        }
        private void btn_SendImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
            op.Title = "请选择文件";
            op.Filter = "所有文件|*.*";
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            byte[] data = FileHelper.GetBytesByFile(op.FileName);
            MyImage file = new MyImage() { ImageName = op.FileName, Data = data };
            string msg = "";
            if (this.allRb.IsChecked == true)
            {
                var keyList = from i in LstUser select i.Key;
                keyList.ToList().ForEach(x => client.SendImageToServer(null, x, file));
                msg = string.Format("我在{0}群发了文件成功：{1}", DateTime.Now, file.ImageName);
            }
            else
            {
                if (this.listBox.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择好友来发送！");
                    return;
                }
                var user = this.listBox.SelectedItem as MyUser;
                string key = user.Key;                    
                client.SendImageToServer(null, key, file);
                msg = string.Format("我在{0}对{1}发送图片成功：{2}", DateTime.Now, user.UserName,file.ImageName);
            }
            UpdateText(msg, Colors.Green, true);

        }
        delegate void deleUpdate(string msg, Color color, bool nextline);
        public void UpdateText(string msg, Color color, bool nextline)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                SolidColorBrush _brushColor = new SolidColorBrush(color);

                string _msg = nextline ? msg + "\r\n" : msg;
                var r = new Run(_msg);
                Paragraph p = new Paragraph() { Foreground = _brushColor };
                p.Inlines.Add(r);
                this.richTextBox.Document.Blocks.Add(p);
                this.richTextBox.Focus();
                this.richTextBox.ScrollToEnd();
            }));
        }
       
        /// <summary>
        /// 客户端接收文件消息
        /// </summary>
        /// <param name="fromKey">发送者</param>
        /// <param name="ToKey">接收者</param>
        /// <param name="image"></param>
        void server_ReceiveImageEvent(string fromKey,string ToKey,MyImage image)
        {
            MyUser fromUser = LstUser.FirstOrDefault(x => x.Key == fromKey);
            if (MessageBox.Show(string.Format("{0}给你发送了一张图片：\r\n{1}\r\n是否接收？", fromUser.UserName,System.IO.Path.GetFileName(image.ImageName)), "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                return;
            }
            System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
            sd.Title = "请设置保存路劲";
            string extension = System.IO.Path.GetExtension(image.ImageName);
            sd.Filter = string.Format("所发的文件(*{0})|*{0}", extension);
            sd.FileName = System.IO.Path.GetFileName(image.ImageName);      
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (sd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                FileHelper.SaveFileFromBytes(image.Data, sd.FileName);
                string msg=string.Format("{0} 接收来自{1}的图片完毕,保存在{2}",DateTime.Now,fromUser.UserName,sd.FileName);
                UpdateText(msg, Colors.Green, true);
            }));
            
        }
        /// <summary>
        /// 客户端接收文字消息
        /// </summary>
        /// <param name="fromKey">发送者</param>
        /// <param name="Tokey">接收者</param>
        /// <param name="msg"></param>
        void server_ReceiveMsgEvent(string fromKey,string Tokey,string msg)
        {
            UpdateText(msg, Colors.Blue, true);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(factory!=null)
            factory.Close();
        }
    }
}
