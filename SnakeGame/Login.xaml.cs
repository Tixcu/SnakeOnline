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

namespace SnakeGame
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string Token = txtToken.Text;
            SnakeOnlineClient Getter = new SnakeOnlineClient();
            string resp = await Getter.GetNameAsync(Token);

            if (resp == "Unauthorized") // Check if token exists
            {
                MessageBox.Show("Invalid Token");
            } else
            {
                MainWindow main = new MainWindow(Token);
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
        }
    }
}
