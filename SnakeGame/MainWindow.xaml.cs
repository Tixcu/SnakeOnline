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
using RestSharp;
using System.Timers;

namespace SnakeGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 

    public partial class MainWindow : Window
    {
        public string Token;
        public MainWindow(string Token)
        {
            this.Token = Token;
            InitializeComponent();
            Game session = new Game(Board, LabelScore, TopScorer, Token);
            session.StartGame();
        }

        private async void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            SnakeOnlineClient Client = new SnakeOnlineClient();
            switch (e.Key)
            {

                case Key.Right:
                    await Client.SendDirection("Right", Token);
                    break;
                case Key.Left:
                    await Client.SendDirection("Left", Token);
                    break;
                case Key.Up:
                    await Client.SendDirection("Top", Token);
                    break;
                case Key.Down:
                    await Client.SendDirection("Bottom", Token);
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }
    }
}
