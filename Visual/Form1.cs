using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/*
 *  Y LÀ J
 *  X LÀ I
 *  X LÀ WIDTH
 *  Y LÀ CAO
 *  0 chưa có
 *  1 tường 
 *  2 bắt đầu
 *  3 đích
 * */


namespace Visual
{
    public partial class Form1 : Form
    {
        private DrawMode DrawMode;

        private const int CELL_SIZE = 30;

        private int MAZE_WIDTH, MAZE_HEIGHT;

        private Maze Maze;

        private delegate double Heuristic(Cell current);

        private Heuristic heur;

        private Graphics MazeGraphics;

        private Thread ThreadFind;

        private int Sleep;

        private int TimeCouter = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawMode = DrawMode.DrawWall;
            MAZE_WIDTH = pnMaze.Width / CELL_SIZE * CELL_SIZE;

            MAZE_HEIGHT = pnMaze.Height / CELL_SIZE * CELL_SIZE;
            Maze = new Maze(pnMaze.Width / CELL_SIZE, pnMaze.Height / CELL_SIZE);

            Control.CheckForIllegalCrossThreadCalls = false;

            MazeGraphics = pnMaze.CreateGraphics();

            heur = heuristic1;

        }

        #region Paint

        private void DrawGrid(Graphics g)
        {
            int top = 0, left = 0;
            // vẽ ngang
            for (int i = 0; i <= Maze.MazeI; i++)
            {
                g.DrawLine(Pens.DarkSlateGray, 0, top, MAZE_WIDTH, top);
                top += CELL_SIZE;
            }

            // vẽ dọc
            for (int i = 0; i <= Maze.MazeJ; i++)
            {
                g.DrawLine(Pens.DarkSlateGray, left, 0, left, MAZE_HEIGHT);
                left += CELL_SIZE;
            }
        }

        private void BrushCell(int i, int j, Brush color)
        {
            var cellPos = ConvertToXY(i, j);
            MazeGraphics.FillRectangle(color, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
            MazeGraphics.DrawLine(Pens.DarkSlateGray, cellPos.X, cellPos.Y, cellPos.X + CELL_SIZE, cellPos.Y);
            MazeGraphics.DrawLine(Pens.DarkSlateGray, cellPos.X + CELL_SIZE, cellPos.Y, cellPos.X + CELL_SIZE, cellPos.Y + CELL_SIZE);
            MazeGraphics.DrawLine(Pens.DarkSlateGray, cellPos.X, cellPos.Y + CELL_SIZE, cellPos.X + CELL_SIZE, cellPos.Y + CELL_SIZE);
            MazeGraphics.DrawLine(Pens.DarkSlateGray, cellPos.X, cellPos.Y, cellPos.X, cellPos.Y + CELL_SIZE);
        }

        private void pnMaze_Paint(object sender, PaintEventArgs e)
        {
            RePaintCell(e.Graphics);
            DrawGrid(e.Graphics);
        }

        private void RePaintCell(Graphics g)
        {
            Point cellPos;

            for (int i = 0; i < Maze.MazeI; i++)
            {
                for (int j = 0; j < Maze.MazeJ; j++)
                {
                    cellPos = ConvertToXY(i, j);
                    if (Maze.Cells[i, j].Value == CellValue.Goal)
                        g.FillRectangle(Brushes.Red, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Maze.Cells[i, j].Value == CellValue.Start)
                        g.FillRectangle(Brushes.SpringGreen, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Maze.Cells[i, j].Value == CellValue.Neighbor)
                        g.FillRectangle(Brushes.Aqua, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Maze.Cells[i, j].Value == CellValue.Visited)
                        g.FillRectangle(Brushes.Pink, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                    else if (Maze.Cells[i, j].Value == CellValue.Wall)
                        g.FillRectangle(Brushes.Black, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
                }
            }
        }
        #endregion

        #region Maze Mouse Event

        private void pnMaze_MouseDown(object sender, MouseEventArgs e)
        {
            var cellPos = ConvertToIJ(e.Location);
            switch (DrawMode)
            {
                case DrawMode.DrawWall:
                    Maze.SetCellValue(cellPos, CellValue.Wall);
                    BrushCell(cellPos.I, cellPos.J, Brushes.Black);
                    break;
                case DrawMode.DrawStart:
                    if (Maze.StartPosition != null)
                    {
                        BrushCell(Maze.StartPosition.I, Maze.StartPosition.J, Brushes.White);
                    }
                    Maze.SetCellValue(cellPos, CellValue.Start);
                    BrushCell(cellPos.I, cellPos.J, Brushes.SpringGreen);
                    break;
                case DrawMode.DrawGoal:
                    if (Maze.GoalPosition != null)
                        BrushCell(Maze.GoalPosition.I, Maze.GoalPosition.J, Brushes.White);
                    Maze.SetCellValue(cellPos, CellValue.Goal);
                    BrushCell(cellPos.I, cellPos.J, Brushes.Red);
                    break;
                case DrawMode.Delete:
                    Maze.SetCellValue(cellPos, CellValue.None);
                    BrushCell(cellPos.I, cellPos.J, Brushes.White);
                    break;
                default:
                    break;
            }
        }

        private void pnMaze_MouseMove(object sender, MouseEventArgs e)
        {
            var cellPos = ConvertToIJ(e.Location);
            if ((DrawMode != DrawMode.DrawWall || DrawMode != DrawMode.Delete) && (e.Button != MouseButtons.Left || cellPos.I < 0
                || cellPos.I >= Maze.MazeI || cellPos.J < 0 || cellPos.J >= Maze.MazeJ))
            {
                return;
            }

            if (DrawMode == DrawMode.DrawWall)
            {
                Maze.SetCellValue(cellPos, CellValue.Wall);
                BrushCell(cellPos.I, cellPos.J, Brushes.Black);
            }
            else if (DrawMode == DrawMode.Delete)
            {
                Maze.SetCellValue(cellPos, CellValue.None);
                BrushCell(cellPos.I, cellPos.J, Brushes.White);
            }
        }

        #endregion

        #region Control Event

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (Maze.StartPosition == null || Maze.GoalPosition == null)
            {
                MessageBox.Show("Chua chon diem bat dau hoac diem dich");
                return;
            }

            TimeCouter = 0;
            lbTime.Text = "Time: 0 second";
            timer1.Start();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            btnFind.Enabled = false;
            btnStop.Enabled = true;
            tbrSleep.Enabled = false;
            btnClear.Enabled = false;
            ClearFindStep();
            Sleep = tbrSleep.Value * 20;
            ThreadFind = new Thread(() => aStart(Maze, heur));
            ThreadFind.IsBackground = true;
            ThreadFind.Start();
        }

        private void ClearFindStep()
        {
            for (int i = 0; i < Maze.MazeI; i++)
            {
                for (int j = 0; j < Maze.MazeJ; j++)
                {
                    if (Maze.Cells[i, j].Value > CellValue.Wall)
                    {
                        Maze.SetCellValue(new CellPositon { I = i, J = j }, CellValue.None);
                    }
                }
            }
            pnMaze.Invalidate();
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

        private void rbtDelete_CheckedChanged(object sender, EventArgs e)
        {
            DrawMode = DrawMode.Delete;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            heur = heuristic1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            heur = heuristic2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            heur = heuristic3;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            tbrSleep.Enabled = true;
            btnClear.Enabled = true;
            ThreadFind.Abort();
        }

        private void tbrSleep_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbrSleep, (tbrSleep.Value * 20).ToString() +" milli seconds");
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeCouter++;
            lbTime.Text = "Time: " + TimeCouter.ToString() + " seconds";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Maze.MazeI; i++)
            {
                for (int j = 0; j < Maze.MazeJ; j++)
                {
                    if (Maze.Cells[i, j].Value != CellValue.None)
                    {
                        Maze.SetCellValue(new CellPositon { I = i, J = j }, CellValue.None);
                    }
                }
            }

            pnMaze.Invalidate();
        }

        #endregion

        #region Algorithm

        private double heuristic1(Cell current)
        {
            return Math.Abs(current.Position.I - Maze.GoalPosition.I) + Math.Abs(current.Position.J - Maze.GoalPosition.J);
        }

        private double heuristic2(Cell current)
        {
            Point posCur = ConvertToXY(current.Position.I, current.Position.J);
            Point posGoal = ConvertToXY(Maze.GoalPosition.I, Maze.GoalPosition.J);

            return Math.Round(Math.Sqrt(Math.Pow(posCur.X - posGoal.X, 2) + Math.Pow(posCur.Y - posGoal.Y, 2)), 3);
        }

        private double heuristic3(Cell current)
        {
            /*
            dx = abs(current_cell.x – goal.x)
            dy = abs(current_cell.y – goal.y) 
            h = D * (dx + dy) + (D2 - 2 * D) * min(dx, dy)
            */
            Point posCur = ConvertToXY(current.Position.I, current.Position.J);
            Point posGoal = ConvertToXY(Maze.GoalPosition.I, Maze.GoalPosition.J);
            double dx = Math.Abs(posCur.X - posGoal.X);
            double dy = Math.Abs(posCur.Y - posGoal.Y);

            return (dy + dx) + (Math.Sqrt(2) - 2) * Math.Min(dy, dx);
        }

        private void reconstruct_path(Cell current)
        {
            int i = 0;
            timer1.Stop();
            while (true)
            {
                current = current.CameFrom;
                if (current.Position == Maze.StartPosition)
                    break;
                i++;
                //current.Value = 6;
                BrushCell(current.Position.I, current.Position.J, Brushes.Yellow);
                Thread.Sleep(Sleep);
            }
            lbStep.Text = "Step: " + i.ToString();
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            tbrSleep.Enabled = true;
            btnClear.Enabled = true;
        }

        private bool aStart(Maze maze, Heuristic her)
        {
            List<Cell> openSet = new List<Cell>();
            openSet.Add(maze.Cells[maze.StartPosition.I, maze.StartPosition.J]);

            for (int i = 0; i < maze.MazeI; i++)
            {
                for (int j = 0; j < maze.MazeJ; j++)
                {
                    maze.Cells[i, j].gScore = 99999;
                    maze.Cells[i, j].fScore = 99999;
                }
            }

            maze.Cells[maze.StartPosition.I, maze.StartPosition.J].gScore = 0;
            maze.Cells[maze.StartPosition.I, maze.StartPosition.J].fScore = her(maze.Cells[maze.StartPosition.I, maze.StartPosition.J]);

            while (openSet.Count != 0)
            {
                foreach (var current in MinfScore(openSet))
                {
                    if (current.Position == maze.GoalPosition)
                    {
                        BrushCell(current.Position.I, current.Position.J, Brushes.Red);
                        reconstruct_path(current);
                        return true;
                    }

                    if (current.Position != maze.StartPosition)
                    {
                        BrushCell(current.Position.I, current.Position.J, Brushes.Pink);
                        current.Value = CellValue.Visited;
                    }

                    Thread.Sleep(Sleep);

                    openSet.Remove(current);

                    foreach (var neughbor in FindNeighbors(current))
                    {
                        var tentative_gScore = current.gScore + 1;
                        if (tentative_gScore < neughbor.gScore)
                        {
                            neughbor.CameFrom = current;
                            neughbor.gScore = tentative_gScore;
                            neughbor.fScore = neughbor.gScore + her(neughbor);
                            if (!Exists(openSet, neughbor))
                            {
                                if (neughbor.Position != maze.GoalPosition)
                                {
                                    neughbor.Value = CellValue.Neighbor;
                                    BrushCell(neughbor.Position.I, neughbor.Position.J, Brushes.Aqua);
                                }

                                openSet.Add(neughbor);

                                Thread.Sleep(Sleep);
                            }
                        }

                    }
                }
            }
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            tbrSleep.Enabled = true;
            btnClear.Enabled = true;
            timer1.Stop();
            MessageBox.Show("Khong tim duoc");
            return false;
        }

        private bool Exists(List<Cell> list, Cell cell)
        {
            foreach (var item in list)
            {
                if (item == cell)
                    return true;
            }
            return false;
        }

        private List<Cell> FindNeighbors(Cell current)
        {
            List<Cell> nei = new List<Cell>();

            if (current.Position.J + 1 < Maze.MazeJ)
                if (Maze.Cells[current.Position.I, current.Position.J + 1].Value < CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I, current.Position.J + 1]);

            if (current.Position.I - 1 >= 0)
                if (Maze.Cells[current.Position.I - 1, current.Position.J].Value < CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I - 1, current.Position.J]);

            if (current.Position.J - 1 >= 0)
                if (Maze.Cells[current.Position.I, current.Position.J - 1].Value < CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I, current.Position.J - 1]);

            if (current.Position.I + 1 < Maze.MazeI)
                if (Maze.Cells[current.Position.I + 1, current.Position.J].Value < CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I + 1, current.Position.J]);

            return nei;
        }
        

        private List<Cell> MinfScore(List<Cell> open)
        {
            double min = open[0].fScore;
            List<Cell> result = new List<Cell>();
            result.Add(open[0]);

            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fScore < min)
                {
                    result.Clear();
                    result.Add(open[i]);
                    min = open[i].fScore;
                }
                else if (open[i].fScore == min)
                {
                    result.Add(open[i]);
                }
            }

            return result;
        }

        #endregion

        #region Helper

        // dùng cho vòng lặp
        private CellPositon ConvertToIJ(Point location)
        {
            return new CellPositon { I = location.Y / CELL_SIZE, J = location.X / CELL_SIZE };
        }

        // dùng cho vẽ
        private Point ConvertToXY(int i, int j)
        {
            return new Point { Y = i * CELL_SIZE, X = j * CELL_SIZE };
        }

        #endregion
    }

    public enum DrawMode
    {
        DrawWall,
        DrawStart,
        DrawGoal,
        Delete
    }
}
