using System;
using System.Drawing;
using System.Windows.Forms;

namespace A_Start_Visualization
{
    public partial class FrmMain : Form
    {
        private const int CELL_SIZE = 30;
        private int BOARD_WIDTH, BOARD_HEIGHT;

        private DrawMode DrawMode;
        private int[,] BoardData;
        private int boardI, boardJ;

        private bool isDrawWall;
        private CellPositon CellStartPositon, CellGoalPosition;

        public FrmMain()
        {
            InitializeComponent();        
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            boardI = pnBoard.Height / CELL_SIZE;
            boardJ = pnBoard.Width / CELL_SIZE;
            BoardData = new int[boardI + 1, boardJ + 1];
            InitBoardData();
            BOARD_WIDTH = boardJ * CELL_SIZE;
            BOARD_HEIGHT = boardI * CELL_SIZE;
            Control.CheckForIllegalCrossThreadCalls = false;
            CellStartPositon = new CellPositon { I = -1 };
            CellGoalPosition = new CellPositon { I = -1 };
        }

        private void DrawBoard(Graphics g)
        {
            Pen pen = new Pen(Color.DarkSlateGray, 1);
            // vẽ ngang 
            for (int i = 0; i <= BOARD_HEIGHT / CELL_SIZE; i++)
            {
                g.DrawLine(pen, 0, i * CELL_SIZE, BOARD_WIDTH, i * CELL_SIZE);
            }

            // vẽ dọc
            for (int i = 0; i <= BOARD_WIDTH / CELL_SIZE; i++)
            {
                g.DrawLine(pen, i * CELL_SIZE, 0, i * CELL_SIZE, BOARD_HEIGHT);
            }
        }

        private void InitBoardData()
        {
            for (int i = 0; i < boardI; i++)
            {
                for (int j = 0; j < boardJ; j++)
                {
                    BoardData[i, j] = 0;
                }
            }

        }

        private void pnBoard_Paint(object sender, PaintEventArgs e)
        {
            BrushCell(e.Graphics);
            DrawBoard(e.Graphics);
        }

        private void pnBoard_MouseDown(object sender, MouseEventArgs e)
        {
            var cellPos = GetCellPositionBoard(e.Location);
            if (BoardData[cellPos.I, cellPos.J] != 0)
                return;
            switch (DrawMode)
            {
                case DrawMode.DrawWall:                     
                    BoardData[cellPos.I, cellPos.J] = 1;
                    isDrawWall = true;
                    break;
                case DrawMode.DrawStart:
                    if (CellStartPositon.I != -1)
                        BoardData[CellStartPositon.I, CellStartPositon.J] = 0;
                    BoardData[cellPos.I, cellPos.J] = 2;
                    CellStartPositon = cellPos;
                    break;
                case DrawMode.DrawGoal:
                    if (CellGoalPosition.I != -1)
                        BoardData[CellGoalPosition.I, CellGoalPosition.J] = 0;
                    BoardData[cellPos.I, cellPos.J] = 3;
                    CellGoalPosition = cellPos;
                    break;
                default:
                    break;
            }

            pnBoard.Invalidate();
        }

        private void pnBoard_MouseMove(object sender, MouseEventArgs e)
        {
            var cellPos = GetCellPositionBoard(e.Location);
            if (!isDrawWall || e.Location.X < 0 || e.Location.X > BOARD_WIDTH || e.Location.Y < 0 || e.Location.Y > BOARD_HEIGHT 
                || BoardData[cellPos.I, cellPos.J] != 0)
                return;
            
            BoardData[cellPos.I, cellPos.J] = 1;
            pnBoard.Invalidate();
        }

        private void pnBoard_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawWall = false;
        }

        private void rbtDrawWall_CheckedChanged(object sender, EventArgs e)
        {
            DrawMode = DrawMode.DrawWall;
        }

        private void rbtDrawStart_CheckedChanged(object sender, EventArgs e)
        {
            DrawMode = DrawMode.DrawStart;
        }

        private void rbtDrawGoal_CheckedChanged(object sender, EventArgs e)
        {
            DrawMode = DrawMode.DrawGoal;
        }

        private void BrushCell(Graphics g)
        {
            Point cellPos;
            Brush brush = Brushes.White;
            for (int i = 0; i < boardI; i++)
            {
                for (int j = 0; j < boardJ; j++)
                {
                    cellPos = GetCellPositonXY(i, j);
                    if (BoardData[i, j] == 1)
                        g.FillRectangle(Brushes.Black, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (BoardData[i, j] == 2)
                        g.FillRectangle(Brushes.SpringGreen, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (BoardData[i, j] == 3)
                        g.FillRectangle(Brushes.Red, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);

                }
            }
        }

        private Point GetCellPositonXY(int i, int j)
        {
            return new Point { X = j * CELL_SIZE, Y = i * CELL_SIZE };
        }

        private CellPositon GetCellPositionBoard(Point location)
        {
            return new CellPositon { I = location.Y / CELL_SIZE, J = location.X / CELL_SIZE };
        }
    }

    public struct CellPositon
    {
        public int I { get; set; }
        public int J { get; set; }
    }

    public enum DrawMode
    {
        DrawWall,
        DrawStart,
        DrawGoal
    }
}
