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

        private int boardI, boardJ;

        private Maze Board;
        private Graphics mazeGrapics;
        private Cell Current;
        private bool isFind = false;

        public delegate double Heuristic(Maze board, Cell current);

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            boardI = pnBoard.Height / CELL_SIZE;
            boardJ = pnBoard.Width / CELL_SIZE;
            Board = new Maze(boardJ, boardI);

            BOARD_WIDTH = boardJ * CELL_SIZE;
            BOARD_HEIGHT = boardI * CELL_SIZE;

            Control.CheckForIllegalCrossThreadCalls = false;
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


        private void pnBoard_Paint(object sender, PaintEventArgs e)
        {
            BrushCell(e.Graphics);
            DrawBoard(e.Graphics);
            if (isFind)
                traces(Current, e.Graphics);
        }

        private void pnBoard_MouseDown(object sender, MouseEventArgs e)
        {
            var cellPos = GetCellPositionBoard(e.Location);
            if (Board.Cells[cellPos.I, cellPos.J].Value != 0)
                return;

            switch (DrawMode)
            {
                case DrawMode.DrawWall:
                    Board.SetWall(cellPos.I, cellPos.J);
                    break;
                case DrawMode.DrawStart:
                    Board.StartPos = cellPos;
                    break;
                case DrawMode.DrawGoal:
                    Board.GoalPos = cellPos;
                    break;
                default:
                    break;
            }

            pnBoard.Invalidate();
        }

        private void pnBoard_MouseMove(object sender, MouseEventArgs e)
        {
            var cellPos = GetCellPositionBoard(e.Location);
            if (DrawMode != DrawMode.DrawWall || e.Button != MouseButtons.Left || e.Location.X < 0 || e.Location.X > BOARD_WIDTH
                || e.Location.Y < 0 || e.Location.Y > BOARD_HEIGHT || Board.Cells[cellPos.I, cellPos.J].Value != 0)
                return;
            Board.SetWall(cellPos.I, cellPos.J);
            pnBoard.Invalidate();
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
                    Board.Cells[i, j].Value = 0;
                    Console.Write(Board.Cells[i, j].Value + " ");
                    if (Board.Cells[i, j].Value == 1)
                    {
                        Board.Cells[i, j].Value = 0;
                    }
                }
                Console.WriteLine();
            }
            pnBoard.Invalidate();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(AStart(Board, heuristic))
            {
                isFind = true;
            }
        }

        private void BrushCell(Graphics g)
        {
            Point cellPos;

            for (int i = 0; i < boardI; i++)
            {
                for (int j = 0; j < boardJ; j++)
                {
                    cellPos = GetCellPositonXY(i, j);
                    if (Board.Cells[i, j].Value == 1)
                        g.FillRectangle(Brushes.Black, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Board.Cells[i, j].Value == 2)
                        g.FillRectangle(Brushes.SpringGreen, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Board.Cells[i, j].Value == 3)
                        g.FillRectangle(Brushes.Red, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Board.Cells[i, j].Value == 4)
                        g.FillRectangle(Brushes.Pink, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Board.Cells[i, j].Value == 5)
                        g.FillRectangle(Brushes.Aqua, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Board.Cells[i, j].Value == 6)
                        g.FillRectangle(Brushes.Yellow, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);

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

        private void traces(Cell current, Graphics g)
        {
            Point cellPos;
            //g.DrawLine(Pens.Yellow, cellPos.X, cellPos.Y + CELL_SIZE / 2, cellPos.Y + CELL_SIZE, cellPos.Y + CELL_SIZE / 2);
            CellPosition old = current.CameFrom.Position;
            Pen pen = new Pen(Color.Yellow, 3);
            while (current.CameFrom != Board.StartCell)
            {
                //cellPos = GetCellPositonXY(current.CameFrom.Position.I, current.CameFrom.Position.J);
                //if (current.CameFrom.CameFrom.Position.I != current.CameFrom.Position.I)
                //{
                //    if (current.CameFrom.CameFrom.CameFrom.Position.J < current.CameFrom.Position.J)
                //    {
                //        g.DrawLine(pen, cellPos.X + CELL_SIZE / 2, cellPos.Y, cellPos.X + CELL_SIZE / 2, cellPos.Y + CELL_SIZE / 2);
                //        g.DrawLine(pen, cellPos.X + CELL_SIZE / 2, cellPos.Y + CELL_SIZE / 2, cellPos.X + CELL_SIZE, 
                //            cellPos.Y + CELL_SIZE / 2);
                //    }
                //    else if (current.CameFrom.CameFrom.CameFrom.Position.J > current.CameFrom.Position.J)
                //    {
                //        g.DrawLine(pen, cellPos.X + CELL_SIZE / 2, cellPos.Y, cellPos.X + CELL_SIZE / 2, cellPos.Y + CELL_SIZE / 2);
                //        g.DrawLine(pen, cellPos.X + CELL_SIZE / 2, cellPos.Y + CELL_SIZE / 2, cellPos.X, cellPos.Y + CELL_SIZE / 2);
                //    }
                //    else
                //        g.DrawLine(pen, cellPos.X + CELL_SIZE / 2, cellPos.Y, cellPos.X + CELL_SIZE / 2, cellPos.Y + CELL_SIZE);
                //}
                //else if (current.CameFrom.CameFrom.Position.I == current.CameFrom.Position.I)
                //{
                //    g.DrawLine(pen, cellPos.X, cellPos.Y + CELL_SIZE / 2, cellPos.X + CELL_SIZE, cellPos.Y + CELL_SIZE / 2);
                //}
                current.CameFrom.Value = 6;
                current.CameFrom = current.CameFrom.CameFrom;

            }
            pnBoard.Invalidate();

        }

        private bool AStart(Maze board, Heuristic h)
        {
            List<Cell> close = new List<Cell>();
            List<Cell> open = new List<Cell>();
            close.Add(board.Cells[board.StartPos.I, board.StartPos.J]);
            open.Add(board.Cells[board.StartPos.I, board.StartPos.J]);
            board.Cells[board.StartPos.I, board.StartPos.J].gScore = 0;
            board.Cells[board.StartPos.I, board.StartPos.J].fScore = 0;
            Board.InitNeighbor();

            while (close.Count != 0)
            {
                foreach (var item in FindNext(close))
                {
                    if (item == board.GoalCell)
                    {
                        Current = item;
                        pnBoard.Invalidate();
                        return true;
                    }
                    if (item != Board.StartCell)
                        item.Value = 5;
                    
                    close.Remove(item);

                    foreach (var neighbor in item.Neighbors)
                    {
                        // f = g + h
                        if (neighbor.CameFrom == null)
                            neighbor.CameFrom = item;
                        // thieu g
                        double he = h(board, neighbor);
                        neighbor.fScore = Math.Round(he, 1);
                        if (!Exits(open, neighbor))
                        {
                            close.Add(neighbor);
                            if (neighbor.Value == 0)
                                neighbor.Value = 4;
                            
                        }
                        open.Add(neighbor);
                    }

                }
            }
            pnBoard.Invalidate();
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
            if (StartCell != null)
            {
                StartCell.Value = 0;
            }
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
            if (GoalCell != null)
            {
                GoalCell.Value = 0;
            }
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


