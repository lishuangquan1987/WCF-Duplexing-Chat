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
using System.Windows.Shapes;

namespace WCF_双工_Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_UserName.Text == "")
                return;
            this.DialogResult = true;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
                Button_Click(this, e);
            base.OnKeyDown(e);
        }
    }
}
