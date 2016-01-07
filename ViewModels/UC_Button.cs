using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Gomoku2.ViewModels
{
    class UC_Button
    {
        public int _x; //tọa độ cột
        public int _y; //tọa độ dòng
        
        public Point _p;

        public Point P
        {
            get { return _p; }
            set
            {
                if (value != null)
                {
                    _p = value;
                    if (button_Click != null)
                        button_Click(_brush, _p);
                }
            }
        }

        ImageBrush _brush;
        public ImageBrush brush
        {
            get { return _brush; }
            set
            {
                if (value != null)
                {
                    _brush = value;
                    if(button_Click != null)
                    {
                        button_Click(_brush, P);
                    }
                }
            }
        }

        public delegate void ClickHandler(ImageBrush new_Value, Point point);
        public event ClickHandler button_Click;
    }
}
