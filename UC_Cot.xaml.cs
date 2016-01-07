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
using Gomoku2.ViewModels;
using Gomoku2.Views;
using System.Windows.Shapes;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json.Linq;

namespace Gomoku2
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    /// 


    public partial class UC_Cot : UserControl
    {

        public int _x { get; set; } //tọa độ cột

        public int _y { get; set; } //tọa độ dòng

        public int active_player { get; set; } //những button trong cột này thuộc lượt đi của người 1 hay 2

        public int win;

        public int p_owner; // thuộc tính quy định ô cờ thuộc về player or AI

        public int row;

        public int col;

        public int type; // thuộc tính phân loại chơi PvP hay PvAI

        UC_Button ViewModel;

        public UC_Cot()
        {
            InitializeComponent();
            ViewModel = new UC_Button();
            ViewModel.button_Click += btn_Click;
            active_player = 0;
            win = 0;
            type = 0;
            p_owner = 0;
            row = 0;
            col = 0;
            _y = 0;
            _x = 0;          
        }
       
        public void newGame()
        {
            active_player = 0;
            win = 0;
            type = 0;
            p_owner = 0;
            row = 0;
            col = 0;
            btn_c1.Background = Brushes.Gray;
            btn_c1.IsHitTestVisible = true;
            btn_c2.Background = Brushes.Blue;
            btn_c2.IsHitTestVisible = true;
            btn_c3.Background = Brushes.Gray;
            btn_c3.IsHitTestVisible = true;
            btn_c4.Background = Brushes.Blue;
            btn_c4.IsHitTestVisible = true;
            btn_c5.Background = Brushes.Gray;
            btn_c5.IsHitTestVisible = true;
            btn_c6.Background = Brushes.Blue;
            btn_c6.IsHitTestVisible = true;
            btn_c7.Background = Brushes.Gray;
            btn_c7.IsHitTestVisible = true;
            btn_c8.Background = Brushes.Blue;
            btn_c8.IsHitTestVisible = true;
            btn_c9.Background = Brushes.Gray;
            btn_c9.IsHitTestVisible = true;
            btn_c10.Background = Brushes.Blue;
            btn_c10.IsHitTestVisible = true;
            btn_c11.Background = Brushes.Gray;
            btn_c11.IsHitTestVisible = true;
            btn_c12.Background = Brushes.Blue;
            btn_c12.IsHitTestVisible = true;
        }

        public delegate void Cot_ClickedHandler(Point p);
        public event Cot_ClickedHandler Cot_Click;

        public delegate void Cot_ClickedHandler3(Point p);
        public event Cot_ClickedHandler3 Cot_Click3;

        private void btn_Click(ImageBrush new_Value, Point point)
        {
            if (win == 1)
                MessageBox.Show("Người chơi 1 đã thắng");
            else
                if (win == 2)
                    MessageBox.Show("Người chơi 2 đã thắng");

            switch (_y)
            {
                case 1:
                    btn_c1.Background = new_Value;
                    btn_c1.IsHitTestVisible = false;
                    break;
                case 2:
                    btn_c2.Background = new_Value;
                    btn_c2.IsHitTestVisible = false;
                    break;
                case 3:
                    btn_c3.Background = new_Value;
                    btn_c3.IsHitTestVisible = false;
                    break;
                case 4:
                    btn_c4.Background = new_Value;
                    btn_c4.IsHitTestVisible = false;
                    break;
                case 5:
                    btn_c5.Background = new_Value;
                    btn_c5.IsHitTestVisible = false;
                    break;
                case 6:
                    btn_c6.Background = new_Value;
                    btn_c6.IsHitTestVisible = false;
                    break;
                case 7:
                    btn_c7.Background = new_Value;
                    btn_c7.IsHitTestVisible = false;
                    break;
                case 8:
                    btn_c8.Background = new_Value;
                    btn_c8.IsHitTestVisible = false;
                    break;
                case 9:
                    btn_c9.Background = new_Value;
                    btn_c9.IsHitTestVisible = false;
                    break;
                case 10:
                    btn_c10.Background = new_Value;
                    btn_c10.IsHitTestVisible = false;
                    break;
                case 11:
                    btn_c11.Background = new_Value;
                    btn_c11.IsHitTestVisible = false;
                    break;
                case 12:
                    btn_c12.Background = new_Value;
                    btn_c12.IsHitTestVisible = false;
                    break;
                default:
                    break;
            }
        }

        private void btn_c1_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 1; // _y là thứ tự dòng _x là thứ tự cột
               
                Point temp = new Point(_y, _x); // đổi lại ở bước này
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 1; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này                    
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else 
                    if(type == 3)
                    {
                        _y = 1;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c2_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 2;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 2; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này   
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 2;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c3_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 3;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 3; // _y là thứ tự dòng _x là thứ tự cột
                    
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 3;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c4_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 4;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 4; // _y là thứ tự dòng _x là thứ tự cột

                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 4;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }
        private void btn_c5_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 5;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 5; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 5;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c6_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 6;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 6; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 6;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c7_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 7;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 7; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 7;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c8_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 8;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 8; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này                   
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 8;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c9_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 9;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 9; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 9;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c10_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 10;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);

                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 10; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 10;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c11_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 11;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 11; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 11;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }

        private void btn_c12_Click(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                _y = 12;
                Point temp = new Point(_y, _x);
                ViewModel.P = temp;
                Cot_Click(ViewModel.P);
                if (active_player == 1)
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                }
                else
                {
                    ViewModel.brush = new ImageBrush();
                    ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                }
            }
            else
                if (type == 1)
                {
                    _y = 12; // _y là thứ tự dòng _x là thứ tự cột
                    Point temp = new Point(_y, _x); // đổi lại ở bước này
                    Cot_Click(temp);
                    if (p_owner == 1)
                    {
                        ViewModel.P = temp;
                        ViewModel.brush = new ImageBrush();
                        ViewModel.brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                    }
                }
                else
                    if (type == 3)
                    {
                        _y = 12;
                        Point temp = new Point(_y, _x);
                        Cot_Click3(temp);
                    }
        }
    }
}
