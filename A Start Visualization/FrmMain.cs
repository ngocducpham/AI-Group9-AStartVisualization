using System;
using System.Collections.Generic;
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
        private CellPosition CellStartPositon, CellGoalPosition;

        public delegate double Heuristic(Maze board, Cell current);

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            boardI = pnBoard.Height / CELL_SIZE;
            boardJ = pnBoard.Width / CELL_SIZE;
            BoardData = new int[boardI, boardJ];
            MessageBox.Show(BoardData.GetLength(0).ToString() + " " + BoardData.GetLength(1));
            InitBoardData();
            BOARD_WIDTH = boardJ * CELL_SIZE;
            BOARD_HEIGHT = boardI * CELL_SIZE;
            Control.CheckForIllegalCrossThreadCalls = false;
            CellStartPositon = new CellPosition { I = -1 };
            CellGoalPosition = new CellPosition { I = -1 };
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

        private void btnClearWall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < boardI; i++)
            {
                for (int j = 0; j < boardJ; j++)
                {
                    if (BoardData[i, j] == 1)
                    {
                        BoardData[i, j] = 0;
                    }
                }
            }
            pnBoard.Invalidate();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

        }

        private void BrushCell(Graphics g)
        {
            Point cellPos;
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

        // i,j to point for draw
        private Point GetCellPositonXY(int i, int j)
        {
            return new Point { X = j * CELL_SIZE, Y = i * CELL_SIZE };
        }

        // point to i,j in for loop
        private CellPosition GetCellPositionBoard(Point location)
        {
            return new CellPosition { I = location.Y / CELL_SIZE, J = location.X / CELL_SIZE };
        }

        private double heuristic(Maze board, Cell current)
        {
            return Math.Sqrt(Math.Pow(current.Position.I - board.GoalCell.Position.I, 2) +
                Math.Pow(current.Position.J - board.GoalCell.Position.J, 2));
        }

        //private void traces(Cell current)
        //{
        //    string path = $"({Board.GoalPos.I},{Board.GoalPos.J}) -> ";
        //    while (current.CameFrom != Board.StartCell)
        //    {
        //        path += $"({current.CameFrom.Position.I},{current.CameFrom.Position.J}) -> ";
        //        current.CameFrom = current.CameFrom.CameFrom;
        //    }
        //    Console.WriteLine(path + $"({Board.StartPos.I},{Board.StartPos.J})");
        //}

        private bool AStart(Maze board, Heuristic h)
        {
            List<Cell> close = new List<Cell>();
            List<Cell> open = new List<Cell>();
            close.Add(board.Cells[board.StartPos.I, board.StartPos.J]);
            open.Add(board.Cells[board.StartPos.I, board.StartPos.J]);
            board.Cells[board.StartPos.I, board.StartPos.J].gScore = 0;
            board.Cells[board.StartPos.I, board.StartPos.J].fScore = 0;
            while (close.Count != 0)
            {
                foreach (var item in FindNext(close))
                {
                    if (item == board.GoalCell)
                    {
                        //traces(item);
                        return true;
                    }
                    item.Value = 5;

                    close.Remove(item);

                    foreach (var neighbor in item.Neighbors)
                    {
                        if (neighbor.CameFrom == null)
                            neighbor.CameFrom = item;
                        double he = h(board, neighbor);
                        neighbor.fScore = Math.Round(he, 1);
                        if (!Exits(open, neighbor))
                        {
                            close.Add(neighbor);
                            neighbor.Value = 4;
                        }
                        open.Add(neighbor);
                    }

                }
            }
            return false;
        }

        private bool Exits(List<Cell> openSet, Cell cell)
        {
            foreach (var item in openSet)
            {
                if (item.Position.I == cell.Position.I && item.Position.J == cell.Position.J)
                    return true;
            }
            return false;
        }

        private List<Cell> FindNext(List<Cell> openSet)
        {
            var listF = new List<Cell>();
            double fMin = openSet[0].fScore;
            listF.Add(openSet[0]);
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fScore < fMin)
                {
                    listF.Clear();
                    listF.Add(openSet[i]);
                    fMin = openSet[i].fScore;
                }
                else if (openSet[i].fScore == fMin)
                    listF.Add(openSet[i]);
            }
            return listF;
        }
    }
}

public struct CellPosition
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

public class Maze
{
    public Cell[,] Cells;
    public readonly int WIDTH, HEIGHT;

    public Cell StartCell;
    public Cell GoalCell;

    private CellPosition start;
    public CellPosition StartPos
    {
        get => start;
        set
        {
            start = value;
            Cells[value.I, value.J].Value = 2;
            StartCell = Cells[value.I, value.J];
        }
    }

    private CellPosition end;
    public CellPosition GoalPos
    {
        get => end;
        set
        {
            end = value;
            Cells[value.I, value.J].Value = 3;
            GoalCell = Cells[value.I, value.J];
        }
    }

    public Maze(int width, int height)
    {
        Cells = new Cell[height, width];
        WIDTH = width;
        HEIGHT = height;

        InitData();
    }

    public void SetWall(int i, int j)
    {
        Cells[i, j].Value = 1;
    }

    private void InitData()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                Cells[i, j] = new Cell();
                Cells[i, j].Value = 0;
                Cells[i, j].Position = new CellPosition { I = i, J = j };
            }
        }


    }

    public void InitNeighbor()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                if (Cells[i, j].Value == 1)
                    continue;
                if (i - 1 >= 0 && Cells[i - 1, j].Value != 1)
                    Cells[i, j].Neighbors.Add(Cells[i - 1, j]);
                if (i + 1 < HEIGHT && Cells[i + 1, j].Value != 1)
                    Cells[i, j].Neighbors.Add(Cells[i + 1, j]);
                if (j - 1 >= 0 && Cells[i, j - 1].Value != 1)
                    Cells[i, j].Neighbors.Add(Cells[i, j - 1]);
                if (j + 1 < WIDTH && Cells[i, j + 1].Value != 1)
                    Cells[i, j].Neighbors.Add(Cells[i, j + 1]);
            }
        }
    }
}

public class Cell
{
    public CellPosition Position;
    public List<Cell> Neighbors;
    public Cell CameFrom;
    public int Value;
    public int gScore;
    public double fScore, hScore;

    public Cell()
    {
        Neighbors = new List<Cell>();
        Position = new CellPosition();
    }
}


