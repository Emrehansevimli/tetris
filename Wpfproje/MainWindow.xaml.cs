using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Tetris
{
    public class board
    {
        private int Rows;
        private int Cols;
        private int Score;
        private int LinesFilled;
        private Tetramina currTetramina;
        private System.Windows.Controls.Label[,] BlockControls;
        static private Brush NoBrush = Brushes.Transparent;
        static private Brush SilverBrush = Brushes.Gray;

        public board(Grid TetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Cols = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            BlockControls = new System.Windows.Controls.Label[Cols, Rows];
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new System.Windows.Controls.Label();
                    BlockControls[i, j].Background = NoBrush;
                    BlockControls[i, j].BorderBrush = SilverBrush;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);

                }
            }
            currTetramina = new Tetramina();
            currTetraminaDraw();
        }
        public int getScore()
        {
            return Score;

        }
        public int getLines()
        {
            return LinesFilled;
        }
        private void currTetraminaDraw()
        {
            Point position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            Brush Color = currTetramina.getcurrColor();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + position.Y) + 2].Background = Color;
            }
        }
        private void currTetraminaErase()
        {
            Point position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + position.Y) + 2].Background = NoBrush;
            }
        }
        private void CheckRows()
        {
            bool full;
            for (int i = Rows - 1; i > 0; i--)
            {
                full = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (BlockControls[j, i].Background == NoBrush)
                    {
                        full = false;
                    }
                }
                if (full)
                {
                    RemoveRow(i);
                    Score += 100;
                    LinesFilled += 1;
                }
            }

        }
        private void RemoveRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }
        public void CurrtetraminamoveLeft()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            currTetraminaErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movleft();
                currTetraminaDraw();

            }
            else
            {
                currTetraminaDraw();
            }
        }
        public void CurrtetraminamoveRight()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            currTetraminaErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1) >= Cols)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movright();
                currTetraminaDraw();

            }
            else
            {
                currTetraminaDraw();
            }
        }
        public void CurrtetraminamoveDown()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            currTetraminaErase();
            foreach (Point S in Shape)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movdown();
                currTetraminaDraw();

            }
            else
            {
                currTetraminaDraw();
                CheckRows();
                currTetramina = new Tetramina();
            }
        }
        public void CurrtetraminamoveRotate()
        {
            Point Position = currTetramina.getCurrPosition();
            Point[] S = new Point[4];
            Point[] Shape = currTetramina.getCurrShape();
            bool move = true;
            Shape.CopyTo(S, 0);
            currTetraminaErase();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)((S[i].Y + Position.Y) + 2)) >= Rows)
                {
                    move = false;

                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                currTetramina.movrotate();
                currTetraminaDraw();

            }
            else
            {
                currTetraminaDraw();
            }
        }
    }

    public class Tetramina
    {// diğer sınıflardan erişebilmek ve belirli bir düzen oluşturup işleri hızlandırmak için sınıf oluşturduk
        private Point currPosition;
        private Point[] currShape;
        private Brush currColor;
        private bool rotate;

        public Tetramina()
        {
            currPosition = new Point(0, 0);
            currColor = Brushes.Transparent;
            currShape = SetRandomShape();
        }
        public Brush getcurrColor()
        {
            return currColor;
        }
        public Point getCurrPosition()
        {
            return currPosition;
        }
        public Point[] getCurrShape()
        {
            return currShape;
        }
        public void movleft()
        {
            currPosition.X -= 1;
        }
        public void movright()
        {
            currPosition.X += 1;
        }
        public void movdown()
        {
            currPosition.Y += 1;
        }
        public void movrotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }

            }
        }



        private Point[] SetRandomShape()
        {
            Random rand = new Random();
            switch (rand.Next() % 7)
            {//kordinat düzleminde noktalar olşuturup şekil elde edicez.
                case 0://I
                    rotate = true;
                    currColor = Brushes.Cyan;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(2,0)

                    };
                case 1://J
                    rotate = true;
                    currColor = Brushes.Blue;
                    return new Point[]
                    {
                        new Point(1,-1),
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0)

                    };
                case 2://L 
                    rotate = true;
                    currColor = Brushes.Orange;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(1,-1)

                    };
                case 3://O
                    rotate = false;
                    currColor = Brushes.Yellow;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(1,0),
                        new Point(0,1),
                        new Point(1,1)

                    };
                case 4://S
                    rotate = true;
                    currColor = Brushes.Green;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,0)

                    };
                case 5://T
                    rotate = true;
                    currColor = Brushes.Purple;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,1),
                        new Point(1,1)

                    };
                case 6://Z
                    rotate = true;
                    currColor = Brushes.Red;
                    return new Point[]
                    {
                        new Point(0,0),
                        new Point(1,0),
                        new Point(0,1),
                        new Point(-1,1)

                    };
                default:
                    return null;
            }
        }
    }



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer;
        board myBoard;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
        

        void MainWindow_Initilized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            GameStart();
        }
        private void GameStart()
        {
            MainGrid.Children.Clear();
            myBoard = new board(MainGrid);
            Timer.Start();

        }
        void GameTick(object sender, EventArgs e)
        {
            Scores.Content = myBoard.getScore().ToString("0000000000");
            Lines.Content = myBoard.getLines().ToString("0000000000");
            myBoard.CurrtetraminamoveDown();

        }
        private void GamePause()
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if(Timer.IsEnabled) myBoard.CurrtetraminamoveLeft();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) myBoard.CurrtetraminamoveRight();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) myBoard.CurrtetraminamoveDown();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) myBoard.CurrtetraminamoveRotate();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F3:
                    GamePause();
                    break;

                default:
                    break;
            }
        }  
    }
}
