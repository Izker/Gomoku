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
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace Gomoku2.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public const int n = 13;
        public int active_player;
        public int sum;
        public int[,] cells = new int[n, n];
        public int win;
        private int count = 0; // thuộc tính đếm số quân cờ
        private int type; // thuộc tính quy định chơi vs người hay chơi vs máy
        private int p_owner; // thuộc tính quy định ô cờ thuộc về player or AI
        private int ai_owner;

        public delegate void Choicehandler(int row, int col);
        public event Choicehandler SetAILoc;

        public MainWindow()
        {
            InitializeComponent();
            Column1.Cot_Click += new UC_Cot.Cot_ClickedHandler(setTurn);
            Column2.Cot_Click += new UC_Cot2.Cot_ClickedHandler(setTurn);
            Column3.Cot_Click += new UC_Cot.Cot_ClickedHandler(setTurn);
            Column4.Cot_Click += new UC_Cot2.Cot_ClickedHandler(setTurn);
            Column5.Cot_Click += new UC_Cot.Cot_ClickedHandler(setTurn);
            Column6.Cot_Click += new UC_Cot2.Cot_ClickedHandler(setTurn);
            Column7.Cot_Click += new UC_Cot.Cot_ClickedHandler(setTurn);
            Column8.Cot_Click += new UC_Cot2.Cot_ClickedHandler(setTurn);
            Column9.Cot_Click += new UC_Cot.Cot_ClickedHandler(setTurn);
            Column10.Cot_Click += new UC_Cot2.Cot_ClickedHandler(setTurn);
            Column11.Cot_Click += new UC_Cot.Cot_ClickedHandler(setTurn);
            Column12.Cot_Click += new UC_Cot2.Cot_ClickedHandler(setTurn);

            Column1.Cot_Click3 += new UC_Cot.Cot_ClickedHandler3(PlayOnline);
            Column2.Cot_Click3 += new UC_Cot2.Cot_ClickedHandler3(PlayOnline);
            Column3.Cot_Click3 += new UC_Cot.Cot_ClickedHandler3(PlayOnline);
            Column4.Cot_Click3 += new UC_Cot2.Cot_ClickedHandler3(PlayOnline);
            Column5.Cot_Click3 += new UC_Cot.Cot_ClickedHandler3(PlayOnline);
            Column6.Cot_Click3 += new UC_Cot2.Cot_ClickedHandler3(PlayOnline);
            Column7.Cot_Click3 += new UC_Cot.Cot_ClickedHandler3(PlayOnline);
            Column8.Cot_Click3 += new UC_Cot2.Cot_ClickedHandler3(PlayOnline);
            Column9.Cot_Click3 += new UC_Cot.Cot_ClickedHandler3(PlayOnline);
            Column10.Cot_Click3 += new UC_Cot2.Cot_ClickedHandler3(PlayOnline);
            Column11.Cot_Click3 += new UC_Cot.Cot_ClickedHandler3(PlayOnline);
            Column12.Cot_Click3 += new UC_Cot2.Cot_ClickedHandler3(PlayOnline);

            SetAILoc += new Choicehandler(ai_play);
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            active_player = 0;
            sum = 0;
            win = 0;
            type = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cells[i, j] = 0;
                }
            }
        }

        //public void InvokeOrExecute(Action action)
        //{
        //    var d = Application.Current.Dispatcher;
        //    if (d.CheckAccess())
        //        action();
        //    else
        //        d.BeginInvoke(DispatcherPriority.Normal, action);
        //}
        //InvokeOrExecute(() => putACheckOnBoard((int)row, (int)col, (int)player+1));
        

        #region Artificial intelligent

        public long[] aScore = new long[7] { 0, 3, 24, 192, 1536, 12288, 98304 };
        public long[] dScore = new long[7] { 0, 1, 9, 81, 729, 6561, 59849 };
        private void ai_play(int row, int col)
        {
            ImageBrush brush;
            switch (col)
            {
                #region col = 1
                case 1:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c1.Background = brush;
                            Column1.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c2.Background = brush;
                            Column1.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c3.Background = brush;
                            Column1.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c4.Background = brush;
                            Column1.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c5.Background = brush;
                            Column1.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c6.Background = brush;
                            Column1.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c7.Background = brush;
                            Column1.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c8.Background = brush;
                            Column1.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c9.Background = brush;
                            Column1.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c10.Background = brush;
                            Column1.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c11.Background = brush;
                            Column1.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column1.btn_c12.Background = brush;
                            Column1.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 2
                case 2:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c1.Background = brush;
                            Column2.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c2.Background = brush;
                            Column2.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c3.Background = brush;
                            Column2.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c4.Background = brush;
                            Column2.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c5.Background = brush;
                            Column2.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c6.Background = brush;
                            Column2.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c7.Background = brush;
                            Column2.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c8.Background = brush;
                            Column2.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c9.Background = brush;
                            Column2.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c10.Background = brush;
                            Column2.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c11.Background = brush;
                            Column2.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column2.btn_c12.Background = brush;
                            Column2.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 3
                case 3:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c1.Background = brush;
                            Column3.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c2.Background = brush;
                            Column3.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c3.Background = brush;
                            Column3.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c4.Background = brush;
                            Column3.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c5.Background = brush;
                            Column3.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c6.Background = brush;
                            Column3.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c7.Background = brush;
                            Column3.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c8.Background = brush;
                            Column3.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c9.Background = brush;
                            Column3.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c10.Background = brush;
                            Column3.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c11.Background = brush;
                            Column3.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column3.btn_c12.Background = brush;
                            Column3.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 4
                case 4:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c1.Background = brush;
                            Column4.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c2.Background = brush;
                            Column4.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c3.Background = brush;
                            Column4.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c4.Background = brush;
                            Column4.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c5.Background = brush;
                            Column4.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c6.Background = brush;
                            Column4.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c7.Background = brush;
                            Column4.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c8.Background = brush;
                            Column4.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c9.Background = brush;
                            Column4.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c10.Background = brush;
                            Column4.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c11.Background = brush;
                            Column4.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column4.btn_c12.Background = brush;
                            Column4.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 5
                case 5:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c1.Background = brush;
                            Column5.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c2.Background = brush;
                            Column5.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c3.Background = brush;
                            Column5.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c4.Background = brush;
                            Column5.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c5.Background = brush;
                            Column5.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c6.Background = brush;
                            Column5.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c7.Background = brush;
                            Column5.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c8.Background = brush;
                            Column5.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c9.Background = brush;
                            Column5.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c10.Background = brush;
                            Column5.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c11.Background = brush;
                            Column5.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column5.btn_c12.Background = brush;
                            Column5.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 6
                case 6:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c1.Background = brush;
                            Column6.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c2.Background = brush;
                            Column6.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c3.Background = brush;
                            Column6.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c4.Background = brush;
                            Column6.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c5.Background = brush;
                            Column6.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c6.Background = brush;
                            Column6.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c7.Background = brush;
                            Column6.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c8.Background = brush;
                            Column6.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c9.Background = brush;
                            Column6.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c10.Background = brush;
                            Column6.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c11.Background = brush;
                            Column6.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column6.btn_c12.Background = brush;
                            Column6.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 7
                case 7:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c1.Background = brush;
                            Column7.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c2.Background = brush;
                            Column7.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c3.Background = brush;
                            Column7.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c4.Background = brush;
                            Column7.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c5.Background = brush;
                            Column7.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c6.Background = brush;
                            Column7.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c7.Background = brush;
                            Column7.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c8.Background = brush;
                            Column7.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c9.Background = brush;
                            Column7.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c10.Background = brush;
                            Column7.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c11.Background = brush;
                            Column7.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column7.btn_c12.Background = brush;
                            Column7.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 8
                case 8:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c1.Background = brush;
                            Column8.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c2.Background = brush;
                            Column8.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c3.Background = brush;
                            Column8.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c4.Background = brush;
                            Column8.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c5.Background = brush;
                            Column8.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c6.Background = brush;
                            Column8.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c7.Background = brush;
                            Column8.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c8.Background = brush;
                            Column8.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c9.Background = brush;
                            Column8.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c10.Background = brush;
                            Column8.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c11.Background = brush;
                            Column8.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column8.btn_c12.Background = brush;
                            Column8.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 9
                case 9:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c1.Background = brush;
                            Column9.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c2.Background = brush;
                            Column9.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c3.Background = brush;
                            Column9.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c4.Background = brush;
                            Column9.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c5.Background = brush;
                            Column9.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c6.Background = brush;
                            Column9.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c7.Background = brush;
                            Column9.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c8.Background = brush;
                            Column9.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c9.Background = brush;
                            Column9.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c10.Background = brush;
                            Column9.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c11.Background = brush;
                            Column9.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column9.btn_c12.Background = brush;
                            Column9.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 10
                case 10:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c1.Background = brush;
                            Column10.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c2.Background = brush;
                            Column10.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c3.Background = brush;
                            Column10.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c4.Background = brush;
                            Column10.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c5.Background = brush;
                            Column10.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c6.Background = brush;
                            Column10.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c7.Background = brush;
                            Column10.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c8.Background = brush;
                            Column10.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c9.Background = brush;
                            Column10.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c10.Background = brush;
                            Column10.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c11.Background = brush;
                            Column10.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column10.btn_c12.Background = brush;
                            Column10.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 11
                case 11:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c1.Background = brush;
                            Column11.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c2.Background = brush;
                            Column11.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c3.Background = brush;
                            Column11.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c4.Background = brush;
                            Column11.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c5.Background = brush;
                            Column11.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c6.Background = brush;
                            Column11.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c7.Background = brush;
                            Column11.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c8.Background = brush;
                            Column11.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c9.Background = brush;
                            Column11.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c10.Background = brush;
                            Column11.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c11.Background = brush;
                            Column11.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column11.btn_c12.Background = brush;
                            Column11.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                #region col = 12
                case 12:
                    switch (row)
                    {
                        case 1:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c1.Background = brush;
                            Column12.btn_c1.IsHitTestVisible = false;
                            break;
                        case 2:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c2.Background = brush;
                            Column12.btn_c2.IsHitTestVisible = false;
                            break;
                        case 3:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c3.Background = brush;
                            Column12.btn_c3.IsHitTestVisible = false;
                            break;
                        case 4:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c4.Background = brush;
                            Column12.btn_c4.IsHitTestVisible = false;
                            break;
                        case 5:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c5.Background = brush;
                            Column12.btn_c5.IsHitTestVisible = false;
                            break;
                        case 6:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c6.Background = brush;
                            Column12.btn_c6.IsHitTestVisible = false;
                            break;
                        case 7:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c7.Background = brush;
                            Column12.btn_c7.IsHitTestVisible = false;
                            break;
                        case 8:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c8.Background = brush;
                            Column12.btn_c8.IsHitTestVisible = false;
                            break;
                        case 9:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c9.Background = brush;
                            Column12.btn_c9.IsHitTestVisible = false;
                            break;
                        case 10:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c10.Background = brush;
                            Column12.btn_c10.IsHitTestVisible = false;
                            break;
                        case 11:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c11.Background = brush;
                            Column12.btn_c11.IsHitTestVisible = false;
                            break;
                        case 12:
                            brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                            Column12.btn_c12.Background = brush;
                            Column12.btn_c12.IsHitTestVisible = false;
                            break;
                        default:
                            break;
                    }
                    break;
                #endregion
                default:
                    break;
            }
        }

        private Point ai_FindWay()
        {
            Point res = new Point();
            long max_Mark = 0; //điểm để xác định nước đi

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (cells[i, j] == 0)
                    {
                        long Attackscore = DiemTC_DuyetDoc(i, j) + DiemTC_DuyetNgang(i, j) + DiemTC_DuyetCheoNguoc(i, j) + DiemTC_DuyetCheoXuoi(i, j);
                        long Defensescore = DiemPN_DuyetDoc(i, j) + DiemPN_DuyetNgang(i, j) + DiemPN_DuyetCheoNguoc(i, j) + DiemPN_DuyetCheoXuoi(i, j); ;
                        long tempMark = Attackscore > Defensescore ? Attackscore : Defensescore;
                        if (max_Mark < tempMark)
                        {
                            max_Mark = tempMark;
                            res.X = i;
                            res.Y = j;
                        }
                    }
                }
            }
            return res;
        }
        private long DiemTC_DuyetDoc(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyệt từ dưới lên 
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if(cells[currRow - count, currCol] == 3)
                    SoQuanTa++;
                else if (cells[currRow - count, currCol] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            //duyet từ trên xuống
            for (int count = 1; count < 6 && currRow + count < n; count++)
            {
                if (cells[currRow + count, currCol] == 3)
                    SoQuanTa++;
                else if (cells[currRow + count, currCol] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            Sum -= dScore[SoQuanDich + 1];
            Sum += aScore[SoQuanTa];
            return Sum;
        }
        private long DiemTC_DuyetNgang(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyệt từ phải sang trái
            for (int count = 1; count < 6 && currCol - count >= 0; count++)
            {
                if (cells[currRow, currCol - count] == 3)
                    SoQuanTa++;
                else if (cells[currRow, currCol - count] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            //duyet từ trái sang phải
            for (int count = 1; count < 6 && currCol + count < n; count++)
            {
                if (cells[currRow, currCol + count] == 3)
                    SoQuanTa++;
                else if (cells[currRow, currCol + count] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            Sum -= dScore[SoQuanDich + 1];
            Sum += aScore[SoQuanTa];
            return Sum;
        }
        private long DiemTC_DuyetCheoXuoi(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyệt góc phải trên
            for (int count = 1; count < 6 && currRow - count >= 0 && currCol + count < n; count++)
            {
                if (cells[currRow - count, currCol + count] == 3)
                    SoQuanTa++;
                else if (cells[currRow - count, currCol + count] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            //duyet goc trái dưới
            for (int count = 1; count < 6 && currRow + count < n && currCol - count >= 0; count++)
            {
                if (cells[currRow + count, currCol - count] == 3)
                    SoQuanTa++;
                else if (cells[currRow + count, currCol - count] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            Sum -= dScore[SoQuanDich + 1];
            Sum += aScore[SoQuanTa];
            return Sum;
        }
        private long DiemTC_DuyetCheoNguoc(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyệt góc trái trên
            for (int count = 1; count < 6 && currRow - count >= 0 && currCol - count >= 0; count++)
            {
                if (cells[currRow - count, currCol - count] == 3)
                    SoQuanTa++;
                else if (cells[currRow - count, currCol - count] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }

            //duyet goc phải dưới
            for (int count = 1; count < 6 && currRow + count < n && currCol + count < n; count++)
            {
                if (cells[currRow + count, currCol + count] == 3)
                    SoQuanTa++;
                else if (cells[currRow + count, currCol + count] == 1)
                {
                    SoQuanDich++;
                    break;
                }
                else
                    break;
            }
            if (SoQuanDich == 2)
                return 0;
            Sum -= dScore[SoQuanDich + 1];
            Sum += aScore[SoQuanTa];
            return Sum;
        }
        private long DiemPN_DuyetDoc(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyệt từ dưới lên 
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (cells[currRow - count, currCol] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow - count, currCol] == 1)
                    SoQuanDich++;
                else
                    break;
            }

            //duyet từ trên xuống
            for (int count = 1; count < 6 && currRow + count < n; count++)
            {
                if (cells[currRow + count, currCol] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow + count, currCol] == 1)
                    SoQuanDich++;
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            Sum += dScore[SoQuanDich];
            return Sum;
        }
        private long DiemPN_DuyetNgang(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyệt từ phải sang trái
            for (int count = 1; count < 6 && currCol - count >= 0; count++)
            {
                if (cells[currRow, currCol - count] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow, currCol - count] == 1)
                    SoQuanDich++;
                else
                    break;
            }

            //duyet từ trái sang phải
            for (int count = 1; count < 6 && currCol + count < n; count++)
            {
                if (cells[currRow, currCol + count] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow, currCol + count] == 1)
                    SoQuanDich++;
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            Sum += dScore[SoQuanDich];
            return Sum;
        }
        private long DiemPN_DuyetCheoXuoi(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyet góc phải trên
            for (int count = 1; count < 6 && currRow - count >= 0 && currCol + count < n; count++)
            {
                if (cells[currRow - count, currCol + count] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow - count, currCol + count] == 1)
                    SoQuanDich++;
                else
                    break;
            }

            //duyet góc trái dưới
            for (int count = 1; count < 6 && currRow + count < n && currCol - count >= 0; count++)
            {
                if (cells[currRow + count, currCol - count] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow + count, currCol - count] == 1)
                    SoQuanDich++;
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            Sum += dScore[SoQuanDich];
            return Sum;
        }
        private long DiemPN_DuyetCheoNguoc(int currRow, int currCol)
        {
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            long Sum = 0;

            //duyet góc trái trên
            for (int count = 1; count < 6 && currRow - count >= 0 && currCol - count >= 0; count++)
            {
                if (cells[currRow - count, currCol - count] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow - count, currCol - count] == 1)
                    SoQuanDich++;
                else
                    break;
            }

            //duyet góc phải dưới
            for (int count = 1; count < 6 && currRow + count < n && currCol + count < n; count++)
            {
                if (cells[currRow + count, currCol + count] == 3)
                {
                    SoQuanTa++;
                    break;
                }
                else if (cells[currRow + count, currCol + count] == 1)
                    SoQuanDich++;
                else
                    break;
            }
            if (SoQuanTa == 2)
                return 0;
            Sum += dScore[SoQuanDich];
            return Sum;
        }

        #endregion
        private Point new_ai;
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker setworker = new BackgroundWorker();

        private int ai_x;
        private int ai_y;
        private int _x;
        private int _y;
        private int o_x;
        private int o_y;
        private int AI_turn = 1;
        private int first = 0;
        private void setTurn(Point p)
        {
            #region player vs player
            if (type == 2)
            {
                // _y là số thứ tự cột tương úng vs số thứ tự cột _x trong file xaml do lúc tạo point đã đổi lại vị trí của _x và _y
                int _y = (int)p.Y;
                // tương tự với _x lúc này là số thứ tự dòng
                int _x = (int)p.X;

                //Xét thứ tự người chơi.
                if (sum % 2 == 0)
                {
                    this.active_player = 1;
                    cells[_x, _y] = this.active_player;
                    if (ITestforWin1(_x, _y, this.active_player) == true)
                        win = this.active_player;
                    else
                        if (ITestforWin2(_x, _y, this.active_player) == true)
                            win = this.active_player;
                        else
                            if (ITestforWin3(_x, _y, this.active_player) == true)
                                win = this.active_player;
                            else
                                if (ITestforWin4(_x, _y, this.active_player) == true)
                                    win = this.active_player;
                                else
                                    if (ITestforWin5(_x, _y, this.active_player) == true)
                                        win = this.active_player;
                                    else
                                        if (ITestforWin6(_x, _y, this.active_player) == true)
                                            win = this.active_player;
                                        else
                                            if (ITestforWin7(_x, _y, this.active_player) == true)
                                                win = this.active_player;
                                            else
                                                if (ITestforWin8(_x, _y, this.active_player) == true)
                                                    win = this.active_player;
                    switch (_y)
                    {
                        case 1:
                            Column1.active_player = this.active_player;
                            Column1.win = this.win;
                            break;
                        case 2:
                            Column2.active_player = this.active_player;
                            Column2.win = this.win;
                            break;
                        case 3:
                            Column3.active_player = this.active_player;
                            Column3.win = this.win;
                            break;
                        case 4:
                            Column4.active_player = this.active_player;
                            Column4.win = this.win;
                            break;
                        case 5:
                            Column5.active_player = this.active_player;
                            Column5.win = this.win;
                            break;
                        case 6:
                            Column6.active_player = this.active_player;
                            Column6.win = this.win;
                            break;
                        case 7:
                            Column7.active_player = this.active_player;
                            Column7.win = this.win;
                            break;
                        case 8:
                            Column8.active_player = this.active_player;
                            Column8.win = this.win;
                            break;
                        case 9:
                            Column9.active_player = this.active_player;
                            Column9.win = this.win;
                            break;
                        case 10:
                            Column10.active_player = this.active_player;
                            Column10.win = this.win;
                            break;
                        case 11:
                            Column11.active_player = this.active_player;
                            Column11.win = this.win;
                            break;
                        case 12:
                            Column12.active_player = this.active_player;
                            Column12.win = this.win;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.active_player = 2;
                    cells[_x, _y] = this.active_player;
                    if (ITestforWin1(_x, _y, this.active_player) == true)
                        win = this.active_player;
                    else
                        if (ITestforWin2(_x, _y, this.active_player) == true)
                            win = this.active_player;
                        else
                            if (ITestforWin3(_x, _y, this.active_player) == true)
                                win = this.active_player;
                            else
                                if (ITestforWin4(_x, _y, this.active_player) == true)
                                    win = this.active_player;
                                else
                                    if (ITestforWin5(_x, _y, this.active_player) == true)
                                        win = this.active_player;
                                    else
                                        if (ITestforWin6(_x, _y, this.active_player) == true)
                                            win = this.active_player;
                                        else
                                            if (ITestforWin7(_x, _y, this.active_player) == true)
                                                win = this.active_player;
                                            else
                                                if (ITestforWin8(_x, _y, this.active_player) == true)
                                                    win = this.active_player;

                    switch (_y)
                    {
                        case 1:
                            Column1.active_player = this.active_player;
                            Column1.win = this.win;
                            break;
                        case 2:
                            Column2.active_player = this.active_player;
                            Column2.win = this.win;
                            break;
                        case 3:
                            Column3.active_player = this.active_player;
                            Column3.win = this.win;
                            break;
                        case 4:
                            Column4.active_player = this.active_player;
                            Column4.win = this.win;
                            break;
                        case 5:
                            Column5.active_player = this.active_player;
                            Column5.win = this.win;
                            break;
                        case 6:
                            Column6.active_player = this.active_player;
                            Column6.win = this.win;
                            break;
                        case 7:
                            Column7.active_player = this.active_player;
                            Column7.win = this.win;
                            break;
                        case 8:
                            Column8.active_player = this.active_player;
                            Column8.win = this.win;
                            break;
                        case 9:
                            Column9.active_player = this.active_player;
                            Column9.win = this.win;
                            break;
                        case 10:
                            Column10.active_player = this.active_player;
                            Column10.win = this.win;
                            break;
                        case 11:
                            Column11.active_player = this.active_player;
                            Column11.win = this.win;
                            break;
                        case 12:
                            Column12.active_player = this.active_player;
                            Column12.win = this.win;
                            break;
                        default:
                            break;
                    }
                }
                sum++;
            }
            #endregion
            #region player vs AI
            else
                if (type == 1)
                {
                    // _y là số thứ tự cột tương úng vs số thứ tự cột _x trong file xaml do lúc tạo point đã đổi lại vị trí của _x và _y
                    _y = (int)p.Y;
                    // tương tự với _x lúc này là số thứ tự dòng
                    _x = (int)p.X;
                    this.ai_owner = 3;
                    this.p_owner = 1;
                    cells[_x, _y] = this.p_owner;
                    //Xác định vị trí đánh tiếp theo của AI
                    worker.RunWorkerAsync();
                    #region xét cột được click
                    switch (_y)
                    {
                        case 1:
                            Column1.p_owner = this.p_owner;
                            Column1.win = this.win;
                            break;
                        case 2:
                            Column2.p_owner = this.p_owner;
                            Column2.win = this.win;
                            break;
                        case 3:
                            Column3.p_owner = this.p_owner;
                            Column3.win = this.win;
                            break;
                        case 4:
                            Column4.p_owner = this.p_owner;
                            Column4.win = this.win;
                            break;
                        case 5:
                            Column5.p_owner = this.p_owner;
                            Column5.win = this.win;
                            break;
                        case 6:
                            Column6.p_owner = this.p_owner;
                            Column6.win = this.win;
                            break;
                        case 7:
                            Column7.p_owner = this.p_owner;
                            Column7.win = this.win;
                            break;
                        case 8:
                            Column8.p_owner = this.p_owner;
                            Column8.win = this.win;
                            break;
                        case 9:
                            Column9.p_owner = this.p_owner;
                            Column9.win = this.win;
                            break;
                        case 10:
                            Column10.p_owner = this.p_owner;
                            Column10.win = this.win;
                            break;
                        case 11:
                            Column11.p_owner = this.p_owner;
                            Column11.win = this.win;
                            break;
                        case 12:
                            Column12.p_owner = this.p_owner;
                            Column12.win = this.win;
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            #endregion
        }

       

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetAILoc(ai_x, ai_y);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            new_ai = ai_FindWay();
            ai_x = (int)new_ai.X;
            ai_y = (int)new_ai.Y;
            cells[ai_x, ai_y] = this.ai_owner;
        
            #region kiểm tra
            if (ITestforWin1(_x, _y, this.p_owner) == true)
            {
                win = this.p_owner;
            }
            else
                if (ITestforWin1(ai_x, ai_y, this.ai_owner) == true)
                {
                    win = this.ai_owner;
                }
                else
                    if (ITestforWin2(_x, _y, this.p_owner) == true)
                    {
                        win = this.p_owner;
                    }
                    else
                        if (ITestforWin2(ai_x, ai_y, this.ai_owner) == true)
                        {
                            win = this.ai_owner;
                        }
                        else
                            if (ITestforWin3(_x, _y, this.p_owner) == true)
                            {
                                win = this.p_owner;
                            }
                            else
                                if (ITestforWin3(ai_x, ai_y, this.ai_owner) == true)
                                {
                                    win = this.ai_owner;
                                }
                                else
                                    if (ITestforWin4(_x, _y, this.p_owner) == true)
                                    {
                                        win = this.p_owner;
                                    }
                                    else
                                        if (ITestforWin4(ai_x, ai_y, this.ai_owner) == true)
                                        {
                                            win = this.ai_owner;
                                        }
                                        else
                                            if (ITestforWin5(_x, _y, this.p_owner) == true)
                                            {
                                                win = this.p_owner;
                                            }
                                            else
                                                if (ITestforWin5(ai_x, ai_y, this.ai_owner) == true)
                                                {
                                                    win = this.ai_owner;
                                                }
                                                else
                                                    if (ITestforWin6(_x, _y, this.p_owner) == true)
                                                    {
                                                        win = this.p_owner;
                                                    }
                                                    else
                                                        if (ITestforWin6(ai_x, ai_y, this.ai_owner) == true)
                                                        {
                                                            win = this.ai_owner;
                                                        }
                                                        else
                                                            if (ITestforWin7(_x, _y, this.p_owner) == true)
                                                            {
                                                                win = this.p_owner;
                                                            }
                                                            else
                                                                if (ITestforWin7(ai_x, ai_y, this.p_owner) == true)
                                                                {
                                                                    win = this.ai_owner;
                                                                }
                                                                else
                                                                    if (ITestforWin8(_x, _y, this.p_owner) == true)
                                                                    {
                                                                        win = this.p_owner;
                                                                    }
                                                                    else
                                                                        if (ITestforWin8(ai_x, ai_y, this.ai_owner) == true)
                                                                        {
                                                                            win = this.ai_owner;
                                                                        }
            if (win == 3)
                MessageBox.Show("Máy đã thắng");
            else
                if (win == 1)
                    MessageBox.Show("Người chơi đã thắng");
            #endregion
        }
        #region TestforWin of PvP
        public bool ITestforWin1(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    res = ITestforWin1(si, --sj, active);
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    res = ITestforWin2(si, (sj + count + 1), active);
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin2(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    if (sj < 12)
                        res = ITestforWin2(si, ++sj, active);
                    else
                    {
                        res = false;
                        count = 0;
                        return res;
                    }
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin3(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    res = ITestforWin3(--si, sj, active);
                    
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    res = ITestforWin4((si + count + 1), sj, active);
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin4(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    if (si < 12)
                        res = ITestforWin4(++si, sj, active);
                    else
                    {
                        res = false;
                        count = 0;
                        return res;
                    }
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin5(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    res = ITestforWin5(--si, --sj, active);

                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    res = ITestforWin8((si + count + 1), (sj + count + 1), active);
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin6(int si, int sj, int active)
         {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    if (sj < 12)
                    {
                        res = ITestforWin6(--si, ++sj, active);
                    }
                    else
                    {
                        res = ITestforWin7(si, sj, active);
                        res = false;
                        count = 0;
                        return res;
                    }
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    res = ITestforWin7((si + count + 1), (sj - count - 1), active);
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin7(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    if (si < 12)
                        res = ITestforWin7(++si, --sj, active);
                    else
                    {
                        res = false;
                        count = 0;
                        return res;
                    }
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        public bool ITestforWin8(int si, int sj, int active)
        {
            bool res = false;
            if (sj > 0 && si > 0)
            {
                if (cells[si, sj] == active)
                {
                    count++;
                    if (count == 5)
                    {
                        res = true;
                        count = 0;
                        return res;
                    }
                    if (si < 12 && sj < 12)
                        res = ITestforWin8(++si, ++sj, active);
                    else
                    {
                        res = false;
                        count = 0;
                        return res;
                    }
                }
                if (cells[si, sj] == 2 || cells[si, sj] == 0 || cells[si, sj] == 3)
                {
                    count = 0;
                    return res;
                }
            }
            return res;
        }
        #endregion

        public bool connected = false;

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            string m = txt_message.Text;
            socket.Emit("ChatMessage", m);
        }

        private void PlayOnline(Point p)
        {
            o_x = (int)p.X - 1;
            o_y = (int)p.Y - 1;
            socket.Emit("MyStepIs", JObject.FromObject(new { row = o_y, col = o_x }));
        }

        public Socket socket;
        private string t;
        private string offMes = "";
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            #region start
            if (this.type == 1 || this.type == 2)
                Column1.type = Column2.type = Column3.type = Column4.type = Column5.type = Column6.type = Column7.type = Column8.type = Column9.type = Column10.type = Column11.type = Column12.type = this.type;
            else if (this.type == 3)
                Column1.type = Column2.type = Column3.type = Column4.type = Column5.type = Column6.type = Column7.type = Column8.type = Column9.type = Column10.type = Column11.type = Column12.type = this.type;
            else if (this.type == 4)
                Column1.type = Column2.type = Column3.type = Column4.type = Column5.type = Column6.type = Column7.type = Column8.type = Column9.type = Column10.type = Column11.type = Column12.type = this.type;

            btn_PvP.IsHitTestVisible = true;
            btn_AI.IsHitTestVisible = true;
            btn_OvP.IsHitTestVisible = true;
            t = txt_Name.Text;


            if (this.type == 3)
            {
                if (btn_start.Content.ToString() == "Start")
                {
                    if (t != "")
                    {
                        socket = IO.Socket("ws://gomoku-lajosveres.rhcloud.com:8000");
                        btn_start.Content = "Change";
                    }
                    else
                        txt_History.Text += "Bạn chưa nhập tên. Hãy nhập tên vào khung Your name.";
                }
                else
                    if (btn_start.Content.ToString() == "Change" && t != "")
                        socket.Emit("MyNameIs", t);
                    else
                        txt_History.Text += "Bạn chưa nhập tên. Hãy nhập tên vào khung Your name.";

                socket.On("ChatMessage", (data) =>
                {
                    var jobject = data as JToken;
                    AppSettings.connect.Message += jobject.Value<String>("message") + "  " + DateTime.Now.ToString() + "\n\n";
                    if (((Newtonsoft.Json.Linq.JObject)data)["message"].ToString() == "Welcome!")
                    {
                        socket.Emit("MyNameIs", t);
                        socket.Emit("ConnectToOtherPlayer");
                    }
                });

                socket.On("NextStepIs", (data) =>
                {
                    var jobject = data as JToken;
                    AppSettings.connect.Player = jobject.Value<int>("player");
                    AppSettings.connect.X = jobject.Value<int>("col") + 1;
                    AppSettings.connect.Y = jobject.Value<int>("row") + 1;
                });

                socket.On("EndGame", (data) =>
                {
                    var jobject = data as JToken;
                    AppSettings.connect.Message += jobject.Value<String>("message") + " " + DateTime.Now.ToString() + "\n\n";
                });
            }
            #endregion 
            //Ai đánh online
            else if (this.type == 4)
            {
                if (btn_start.Content.ToString() == "Start")
                {
                    if (t != "")
                    {
                        socket = IO.Socket("ws://gomoku-lajosveres.rhcloud.com:8000");
                        btn_start.Content = "Change";
                    }
                    else
                        txt_History.Text += "Bạn chưa nhập tên. Hãy nhập tên vào khung Your name.";
                }
                else
                    if (btn_start.Content.ToString() == "Change" && t != "")
                        socket.Emit("MyNameIs", t);
                    else
                        txt_History.Text += "Bạn chưa nhập tên. Hãy nhập tên vào khung Your name.";

                socket.On("ChatMessage", (data) =>
                {
                    var jobject = data as JToken;
                    AppSettings.connect.Message += jobject.Value<String>("message") + "  " + DateTime.Now.ToString() + "\n\n";
                    if (((Newtonsoft.Json.Linq.JObject)data)["message"].ToString() == "Welcome!")
                    {
                        socket.Emit("MyNameIs", t);
                        socket.Emit("ConnectToOtherPlayer");
                    }
                    if (AppSettings.connect.Message.Contains("You are the first player"))
                        this.AI_turn = 0;
                    if (((Newtonsoft.Json.Linq.JObject)data)["message"].ToString().Contains("You are the first player"))
                    {
                        Point temp = new Point(6, 6);
                        PlayOnline(temp);
                    }
                });

                socket.On("NextStepIs", (data) =>
                {
                    var jobject = data as JToken;
                    AppSettings.connect.Player = jobject.Value<int>("player");
                    this.AI_turn = AppSettings.connect.Player;
                    AppSettings.connect.X = jobject.Value<int>("col") + 1;
                    AppSettings.connect.Y = jobject.Value<int>("row") + 1;
                });

                socket.On("EndGame", (data) =>
                {
                    var jobject = data as JToken;
                    AppSettings.connect.Message += jobject.Value<String>("message") + " " + DateTime.Now.ToString() + "\n\n";
                });
            }
            else
            {
                offMes += t + " " + DateTime.Now.ToString() + "\n\n";
                txt_History.Text = offMes;
            }
        }

        private void btn_AI_Click(object sender, RoutedEventArgs e)
        {
            this.type = 1;
            btn_PvP.IsHitTestVisible = false;
            btn_start.IsHitTestVisible = true;
        }

        private void btn_PvP_Click(object sender, RoutedEventArgs e)
        {
            this.type = 2;
            btn_AI.IsHitTestVisible = false;
            btn_start.IsHitTestVisible = true;
        }

        private void btn_OvP_Click(object sender, RoutedEventArgs e)
        {
            this.type = 3;
            btn_OvP.IsHitTestVisible = false;
            btn_start.IsHitTestVisible = true;
        }
        
        private void createCheck()
        {
            if (this.type == 3 || this.type == 4)
            {
                int col = AppSettings.connect.X;
                int row = AppSettings.connect.Y;
                int player = AppSettings.connect.Player;
                ImageBrush brush;
                switch (col)
                {
                    #region col = 1
                    case 1:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c1.Background = brush;
                                Column1.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c2.Background = brush;
                                Column1.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c3.Background = brush;
                                Column1.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c4.Background = brush;
                                Column1.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c5.Background = brush;
                                Column1.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c6.Background = brush;
                                Column1.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c7.Background = brush;
                                Column1.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c8.Background = brush;
                                Column1.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c9.Background = brush;
                                Column1.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c10.Background = brush;
                                Column1.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c11.Background = brush;
                                Column1.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column1.btn_c12.Background = brush;
                                Column1.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 2
                    case 2:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c1.Background = brush;
                                Column2.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c2.Background = brush;
                                Column2.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c3.Background = brush;
                                Column2.btn_c3.IsHitTestVisible = false;
                                Column2.btn_c3.Background.Freeze();
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c4.Background = brush;
                                Column2.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c5.Background = brush;
                                Column2.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c6.Background = brush;
                                Column2.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c7.Background = brush;
                                Column2.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c8.Background = brush;
                                Column2.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c9.Background = brush;
                                Column2.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c10.Background = brush;
                                Column2.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c11.Background = brush;
                                Column2.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column2.btn_c12.Background = brush;
                                Column2.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 3
                    case 3:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c1.Background = brush;
                                Column3.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c2.Background = brush;
                                Column3.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c3.Background = brush;
                                Column3.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c4.Background = brush;
                                Column3.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c5.Background = brush;
                                Column3.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c6.Background = brush;
                                Column3.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c7.Background = brush;
                                Column3.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c8.Background = brush;
                                Column3.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c9.Background = brush;
                                Column3.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c10.Background = brush;
                                Column3.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c11.Background = brush;
                                Column3.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column3.btn_c12.Background = brush;
                                Column3.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 4
                    case 4:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c1.Background = brush;
                                Column4.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c2.Background = brush;
                                Column4.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c3.Background = brush;
                                Column4.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c4.Background = brush;
                                Column4.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c5.Background = brush;
                                Column4.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c6.Background = brush;
                                Column4.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c7.Background = brush;
                                Column4.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c8.Background = brush;
                                Column4.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c9.Background = brush;
                                Column4.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c10.Background = brush;
                                Column4.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c11.Background = brush;
                                Column4.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column4.btn_c12.Background = brush;
                                Column4.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 5
                    case 5:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c1.Background = brush;
                                Column5.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c2.Background = brush;
                                Column5.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c3.Background = brush;
                                Column5.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c4.Background = brush;
                                Column5.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c5.Background = brush;
                                Column5.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c6.Background = brush;
                                Column5.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c7.Background = brush;
                                Column5.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c8.Background = brush;
                                Column5.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c9.Background = brush;
                                Column5.btn_c9.IsHitTestVisible = false;
                                //btn_c9.Background.Freeze();
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c10.Background = brush;
                                Column5.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c11.Background = brush;
                                Column5.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column5.btn_c12.Background = brush;
                                Column5.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 6
                    case 6:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c1.Background = brush;
                                Column6.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c2.Background = brush;
                                Column6.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c3.Background = brush;
                                Column6.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c4.Background = brush;
                                Column6.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c5.Background = brush;
                                Column6.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c6.Background = brush;
                                Column6.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c7.Background = brush;
                                Column6.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c8.Background = brush;
                                Column6.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c9.Background = brush;
                                Column6.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c10.Background = brush;
                                Column6.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c11.Background = brush;
                                Column6.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column6.btn_c12.Background = brush;
                                Column6.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 7
                    case 7:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c1.Background = brush;
                                Column7.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c2.Background = brush;
                                Column7.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c3.Background = brush;
                                Column7.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c4.Background = brush;
                                Column7.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c5.Background = brush;
                                Column7.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c6.Background = brush;
                                Column7.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c7.Background = brush;
                                Column7.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c8.Background = brush;
                                Column7.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c9.Background = brush;
                                Column7.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c10.Background = brush;
                                Column7.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c11.Background = brush;
                                Column7.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column7.btn_c12.Background = brush;
                                Column7.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 8
                    case 8:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c1.Background = brush;
                                Column8.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c2.Background = brush;
                                Column8.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c3.Background = brush;
                                Column8.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c4.Background = brush;
                                Column8.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c5.Background = brush;
                                Column8.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c6.Background = brush;
                                Column8.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c7.Background = brush;
                                Column8.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c8.Background = brush;
                                Column8.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c9.Background = brush;
                                Column8.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c10.Background = brush;
                                Column8.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c11.Background = brush;
                                Column8.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column8.btn_c12.Background = brush;
                                Column8.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 9
                    case 9:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c1.Background = brush;
                                Column9.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c2.Background = brush;
                                Column9.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c3.Background = brush;
                                Column9.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c4.Background = brush;
                                Column9.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c5.Background = brush;
                                Column9.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c6.Background = brush;
                                Column9.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c7.Background = brush;
                                Column9.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c8.Background = brush;
                                Column9.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c9.Background = brush;
                                Column9.btn_c9.IsHitTestVisible = false;

                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c10.Background = brush;
                                Column9.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c11.Background = brush;
                                Column9.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column9.btn_c12.Background = brush;
                                Column9.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 10
                    case 10:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c1.Background = brush;
                                Column10.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c2.Background = brush;
                                Column10.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c3.Background = brush;
                                Column10.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c4.Background = brush;
                                Column10.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c5.Background = brush;
                                Column10.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c6.Background = brush;
                                Column10.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c7.Background = brush;
                                Column10.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c8.Background = brush;
                                Column10.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c9.Background = brush;
                                Column10.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c10.Background = brush;
                                Column10.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c11.Background = brush;
                                Column10.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column10.btn_c12.Background = brush;
                                Column10.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 11
                    case 11:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c1.Background = brush;
                                Column11.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c2.Background = brush;
                                Column11.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c3.Background = brush;
                                Column11.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c5.Background = brush;
                                Column11.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c6.Background = brush;
                                Column11.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c7.Background = brush;
                                Column11.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c8.Background = brush;
                                Column11.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c9.Background = brush;
                                Column11.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c10.Background = brush;
                                Column11.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c11.Background = brush;
                                Column11.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column11.btn_c12.Background = brush;
                                Column11.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    #region col = 12
                    case 12:
                        switch (row)
                        {
                            case 1:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c1.Background = brush;
                                Column12.btn_c1.IsHitTestVisible = false;
                                break;
                            case 2:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c2.Background = brush;
                                Column12.btn_c2.IsHitTestVisible = false;
                                break;
                            case 3:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c3.Background = brush;
                                Column12.btn_c3.IsHitTestVisible = false;
                                break;
                            case 4:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c4.Background = brush;
                                Column12.btn_c4.IsHitTestVisible = false;
                                break;
                            case 5:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c5.Background = brush;
                                Column12.btn_c5.IsHitTestVisible = false;
                                break;
                            case 6:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c6.Background = brush;
                                Column12.btn_c6.IsHitTestVisible = false;
                                break;
                            case 7:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c7.Background = brush;
                                Column12.btn_c7.IsHitTestVisible = false;
                                break;
                            case 8:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c8.Background = brush;
                                Column12.btn_c8.IsHitTestVisible = false;
                                break;
                            case 9:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c9.Background = brush;
                                Column12.btn_c9.IsHitTestVisible = false;
                                break;
                            case 10:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c10.Background = brush;
                                Column12.btn_c10.IsHitTestVisible = false;
                                break;
                            case 11:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c11.Background = brush;
                                Column12.btn_c11.IsHitTestVisible = false;
                                break;
                            case 12:
                                if (player == 1)
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/Circle.png", UriKind.Relative));
                                }
                                else
                                {
                                    brush = new ImageBrush();
                                    brush.ImageSource = new BitmapImage(new Uri("../../Picture/X.png", UriKind.Relative));
                                }
                                Column12.btn_c12.Background = brush;
                                Column12.btn_c12.IsHitTestVisible = false;
                                break;
                            default:
                                break;
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
        }

        private void txt_row_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.first++; // vì lúc khởi tạo textchange phat sinh sự kiện khi chuyển giá trị của textbox về 0 nên first = 1
            if (cells[AppSettings.connect.X, AppSettings.connect.Y] != 0)
                return;
            if (AppSettings.connect.Player == 0)
                cells[AppSettings.connect.X, AppSettings.connect.Y] = 1;
            else
                cells[AppSettings.connect.X, AppSettings.connect.Y] = 3;
            
            createCheck();
            if (this.type == 4 && first != 2 && this.AI_turn == 0)
            {
                if (this.AI_turn != AppSettings.connect.Player)
                {
                    new_ai = ai_FindWay();
                    PlayOnline(new_ai);
                }
            }
            else
                if (this.type == 4 && first != 2 && this.AI_turn == 1)
                {
                    if (this.AI_turn == AppSettings.connect.Player)
                    {
                        new_ai = ai_FindWay();
                        PlayOnline(new_ai);
                    }
                }
        }

        private void txt_col_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.first++; // vì lúc khởi tạo textchange phat sinh sự kiện khi chuyển giá trị của textbox về 0 nên first = 1
            if (cells[AppSettings.connect.X, AppSettings.connect.Y] != 0)
                return;
            if (AppSettings.connect.Player == 0)
                cells[AppSettings.connect.X, AppSettings.connect.Y] = 1;
            else
                cells[AppSettings.connect.X, AppSettings.connect.Y] = 3;

            createCheck();
            if (this.type == 4 && first != 2 && this.AI_turn == 0)
            {
                if (this.AI_turn != AppSettings.connect.Player)
                {
                    new_ai = ai_FindWay();
                    PlayOnline(new_ai);
                }
            }
            else
                if (this.type == 4 && first != 2 && this.AI_turn == 1)
                {
                    if (this.AI_turn == AppSettings.connect.Player)
                    {
                        new_ai = ai_FindWay();
                        PlayOnline(new_ai);
                    }
                }
        }

        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            Column1.newGame();
            Column2.newGame();
            Column3.newGame();
            Column4.newGame(); 
            Column5.newGame();
            Column6.newGame(); 
            Column7.newGame();
            Column8.newGame(); 
            Column9.newGame();
            Column10.newGame(); 
            Column11.newGame();
            Column12.newGame();
            btn_AI.IsHitTestVisible = true;
            btn_OvP.IsHitTestVisible = true;
            btn_PvP.IsHitTestVisible = true;
            active_player = 0;
            sum = 0;
            win = 0;
            type = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cells[i, j] = 0;
                }
            }
            btn_start.Content = "Start";
        }

        private void btn_AIOL_Click(object sender, RoutedEventArgs e)
        {
            this.type = 4;
            btn_AIOL.IsHitTestVisible = false;
            btn_start.IsHitTestVisible = true;
        }
    }
}