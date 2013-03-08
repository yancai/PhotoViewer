using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace PhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string _filePath = string.Empty;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckBox_Nine.IsChecked = false;
            Canvas_NineBlock.Children.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg(*.jpg)|*.jpg|bmp(*.bmp)|*.bmp|gif文件(*. gif)|*. gif|png(*.png)|*.png|所有文件(*.*)|*.*"; ;
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            _filePath = openFileDialog.FileName;

            try
            {
                Image_main.Source = new BitmapImage(new Uri(FilePath));
            }
            catch (Exception exception)
            {
                MessageBox.Show("文件打开失败！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_close_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        private void Button_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CheckBox_Nine_Click(object sender, RoutedEventArgs e)
        {

            if (!IsLoadImage())
            {
                return;
            }

            if (Canvas_NineBlock.Children.Count == 0)
            {
                Init_NineBlock();
            }

            if (CheckBox_Nine.IsChecked == true)
            {
                Show_NineBlock();
            }
            else if (CheckBox_Nine.IsChecked == false)
            {
                Hide_NineBlock();
            }

        }

        private void Button_about_Click(object sender, RoutedEventArgs e)
        {
            string about = "本工具是查看相片九宫格构图的小工具\n";
            MessageBox.Show(about, "关于", MessageBoxButton.OK);
        }

        private bool IsLoadImage()
        {
            if (Image_main.Source == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Init_NineBlock()
        {
            var width = Image_main.ActualWidth;
            var height = Image_main.ActualHeight;
            Point point = Image_main.TranslatePoint(new Point(), Grid_image);
            Draw_Block(point.Y, point.X, width, height);

        }

        private void Draw_Block(double top, double left, double width, double height)
        {
            double[] X = new double[4];
            double[] Y = new double[4];

            for (int i = 0; i < 4; i++)
            {
                X[i] = left + width*(i/3.0);
                Y[i] = top + height*(i/3.0);
            }

            Canvas_NineBlock.Children.Add(new Line { X1 = X[0], Y1 = Y[1], X2 = X[3], Y2 = Y[1], Stroke = Brushes.Red, StrokeThickness = 2 });
            Canvas_NineBlock.Children.Add(new Line { X1 = X[0], Y1 = Y[2], X2 = X[3], Y2 = Y[2], Stroke = Brushes.Red, StrokeThickness = 2 });
            Canvas_NineBlock.Children.Add(new Line { X1 = X[1], Y1 = Y[0], X2 = X[1], Y2 = Y[3], Stroke = Brushes.Red, StrokeThickness = 2 });
            Canvas_NineBlock.Children.Add(new Line { X1 = X[2], Y1 = Y[0], X2 = X[2], Y2 = Y[3], Stroke = Brushes.Red, StrokeThickness = 2 });
        }

        private void Hide_NineBlock()
        {
            Canvas_NineBlock.Visibility = Visibility.Hidden;
        }

        private void Show_NineBlock()
        {
            Canvas_NineBlock.Visibility = Visibility.Visible;
        }

        private void Button_test_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
