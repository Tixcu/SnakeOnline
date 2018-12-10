using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SnakeGame
{
    public enum Piece{ Head, Body, Body1, Protected, Food, Free, Wall};

    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; private set; }
        private BitmapImage _Image;
        public Piece Type { get; private set; }

        public Cell(Piece type, int Id)
        {
            ChangeType(type);
            this.Id = Id;
        }

        public void ChangeType (Piece type)
        {
            switch (type)
            {
                case Piece.Free:
                    Image = ImagePeaceType.Free;
                    break;
                case Piece.Head:
                    Image = ImagePeaceType.Head;
                    break;
                case Piece.Body:
                    Image = ImagePeaceType.Body;
                    break;
                case Piece.Body1:
                    Image = ImagePeaceType.Body1;
                    break;
                case Piece.Protected:
                    Image = ImagePeaceType.Protected;
                    break;
                case Piece.Food:
                    Image = ImagePeaceType.Food;
                    break;
                case Piece.Wall:
                    Image = ImagePeaceType.Wall;
                    break;
            }
            this.Type = type;
        }

        public BitmapImage Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private void NotifyPropertyChanged(String PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public static class ImagePeaceType
    {
        static ImagePeaceType()
        {
            Head = new BitmapImage();
            Head.BeginInit();
            Head.UriSource = new Uri("pack://application:,,,/img/head.png");
            Head.EndInit();


            Body = new BitmapImage();
            Body.BeginInit();
            Body.UriSource = new Uri("pack://application:,,,/img/body2.png");
            Body.EndInit();

            Body1 = new BitmapImage();
            Body1.BeginInit();
            Body1.UriSource = new Uri("pack://application:,,,/img/body1.png");
            Body1.EndInit();

            Food = new BitmapImage();
            Food.BeginInit();
            Food.UriSource = new Uri("pack://application:,,,/img/protected.png");
            Food.EndInit();

            Food = new BitmapImage();
            Food.BeginInit();
            Food.UriSource = new Uri("pack://application:,,,/img/food.png");
            Food.EndInit();

            Wall = new BitmapImage();
            Wall.BeginInit();
            Wall.UriSource = new Uri("pack://application:,,,/img/wall.png");
            Wall.EndInit();
        }

        public static BitmapImage Free { get; private set; }
        public static BitmapImage Head { get; private set; }
        public static BitmapImage Body { get; private set; }
        public static BitmapImage Body1 { get; private set; }
        public static BitmapImage Protected { get; private set; }
        public static BitmapImage Food { get; private set; }
        public static BitmapImage Wall { get; private set; }
    }
}
