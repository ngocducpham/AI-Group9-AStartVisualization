using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private int CELL_SIZE = 30;

        private int MAZE_WIDTH, MAZE_HEIGHT;

        private Maze Maze;

        private delegate double Heuristic(Cell current);

        private Heuristic heur;

        private Thread ThreadFind;

        private int Sleep;

        private int TimeCouter = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void InitMaze()
        {
            MAZE_WIDTH = (pnMaze.Width / CELL_SIZE) * CELL_SIZE;
            MAZE_HEIGHT = (pnMaze.Height / CELL_SIZE) * CELL_SIZE;
            Maze = new Maze(pnMaze.Width / CELL_SIZE, pnMaze.Height / CELL_SIZE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawMode = DrawMode.DrawWall;
            InitMaze();

            Control.CheckForIllegalCrossThreadCalls = false;

            heur = ManhattanHeuristic;

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

        private void pnMaze_Paint(object sender, PaintEventArgs e)
        {
            BrushCell(e.Graphics);
            DrawGrid(e.Graphics);
        }

        private void BrushCell(Graphics g)
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
                    else if (Maze.Cells[i, j].Value == CellValue.Path)
                        g.FillRectangle(Brushes.Yellow, cellPos.X, cellPos.Y, CELL_SIZE, CELL_SIZE);
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
                    break;
                case DrawMode.DrawStart:
                    Maze.SetCellValue(cellPos, CellValue.Start);
                    
                    break;
                case DrawMode.DrawGoal:
                    Maze.SetCellValue(cellPos, CellValue.Goal);
                    
                    break;
                case DrawMode.Delete:
                    Maze.SetCellValue(cellPos, CellValue.None);
                    break;
                default:
                    break;
            }

            pnMaze.Invalidate();
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
            }
            else if (DrawMode == DrawMode.Delete)
            {
                Maze.SetCellValue(cellPos, CellValue.None);
            }

            pnMaze.Invalidate();
        }

        #endregion

        #region Control Event

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (Maze.StartCell == null || Maze.GoalCell == null)
            {
                MessageBox.Show("Chưa chọn điểm bắt đầu, kết thúc", "Lỗi!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeCouter = 0;
            lbTime.Text = "Time: 0 second";
            timer1.Start();
            DisableControl();

            ClearFindStep();
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
            heur = ManhattanHeuristic;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            heur = DiagonalHeuristic;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            heur = EuclideanHeuristic;
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
            btnClearStep.Enabled = true;
            txtCellSize.Enabled = true;

            ThreadFind.Abort();
        }

        private void tbrSleep_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbrSleep, (tbrSleep.Value * 20).ToString() + " milli seconds");
            Sleep = tbrSleep.Value * 20;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeCouter++;
            lbTime.Text = "Time: " + TimeCouter.ToString() + " seconds";
        }

        private void btnClearStep_Click(object sender, EventArgs e)
        {
            ClearFindStep();
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

        #region Menu Event

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                FileName = "Maze.maz",
                DefaultExt = "maz",
                Filter = "Maze (*.maz)|*.maz",
                RestoreDirectory = true,
                CheckPathExists = true,
                Title = "Save Maze"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            // cell_size mazeI mazeJ \n data
            string saveData = $"{CELL_SIZE}\n";

            for (int i = 0; i < Maze.MazeI; i++)
            {
                for (int j = 0; j < Maze.MazeJ; j++)
                {
                    if ((int)Maze.Cells[i, j].Value > (int)CellValue.Wall)
                        saveData += "0";
                    else
                        saveData += ((int)Maze.Cells[i, j].Value).ToString();

                    if (i != Maze.MazeI - 1 || j != Maze.MazeJ - 1)
                        saveData += "\n";
                }
            }

            try
            {
                File.WriteAllText(saveDialog.FileName, saveData);
            }
            catch
            {
                MessageBox.Show("Không thể lưu được file", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                DefaultExt = "maz",
                Filter = "Maze (*.maz)|*.maz",
                RestoreDirectory = true,
                CheckPathExists = true,
                Title = "Open Maze"
            };

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                string[] fileContents = File.ReadAllLines(openDialog.FileName);

                CELL_SIZE = int.Parse(fileContents[0]);
                InitMaze();

                int line = 1;
                for (int i = 0; i < Maze.MazeI; i++)
                {
                    for (int j = 0; j < Maze.MazeJ; j++)
                    {
                        if (fileContents[line] != "0")
                        {
                            Maze.SetCellValue(new CellPositon { I = i, J = j }, (CellValue)int.Parse(fileContents[line]));
                        }
                        line++;
                    }
                }

                txtCellSize.Text = fileContents[0];
                pnMaze.Invalidate();
            }
            catch
            {
                MessageBox.Show("Không thể mở được file", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Algorithm
        #region Heuristic
        // http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html#S7

        private double ManhattanHeuristic(Cell current)
        {
            /*
             dx = abs(node.x - goal.x)
             dy = abs(node.y - goal.y)
             h  = dx + dy
             */

            return Math.Abs(current.Position.I - Maze.GoalCell.Position.I) + Math.Abs(current.Position.J - Maze.GoalCell.Position.J);
        }

        private double DiagonalHeuristic(Cell current)
        {
            /*
            dx = abs(current_cell.x – goal.x)
            dy = abs(current_cell.y – goal.y) 
            h  = D * (dx + dy) + (D2 - 2 * D) * min(dx, dy)
            */

            Point posCur = ConvertToXY(current.Position.I, current.Position.J);
            Point posGoal = ConvertToXY(Maze.GoalCell.Position.I, Maze.GoalCell.Position.J);

            double dx = Math.Abs(posCur.X - posGoal.X);
            double dy = Math.Abs(posCur.Y - posGoal.Y);

            return (dy + dx) + (Math.Sqrt(2) - 2) * Math.Min(dy, dx);
        }

        private double EuclideanHeuristic(Cell current)
        {
            /*
             dx = abs(node.x - goal.x)
             dy = abs(node.y - goal.y)
             h  = sqrt(dx * dx + dy * dy)
             */

            Point posCur = ConvertToXY(current.Position.I, current.Position.J);
            Point posGoal = ConvertToXY(Maze.GoalCell.Position.I, Maze.GoalCell.Position.J);

            return Math.Sqrt(Math.Pow(posCur.X - posGoal.X, 2) + Math.Pow(posCur.Y - posGoal.Y, 2));
        }

        #endregion

        private void ReconstructPath(Cell current)
        {
            int i = 0;
            timer1.Stop();
            while (true)
            {
                i++;
                current = current.CameFrom;
                if (current.Position == Maze.StartCell.Position)
                    break;
            
                current.Value = CellValue.Path;

                if (Sleep != 0)
                {
                    pnMaze.Invalidate();
                    Thread.Sleep(Sleep);
                }

            }

            lbPath.Text = "Path: " + i.ToString();
            EnableControl();
        }

        private bool aStart(Maze maze, Heuristic her)
        {
            List<Cell> openSet = new List<Cell>();
            openSet.Add(maze.Cells[maze.StartCell.Position.I, maze.StartCell.Position.J]);

            for (int i = 0; i < maze.MazeI; i++)
            {
                for (int j = 0; j < maze.MazeJ; j++)
                {
                    maze.Cells[i, j].gScore = 99999;
                    maze.Cells[i, j].fScore = 99999;
                }
            }

            maze.StartCell.gScore = 0;
            maze.StartCell.fScore = her(maze.StartCell);

            while (openSet.Count != 0)
            {
                foreach (var current in MinfScore(openSet))
                {
                    if (current.Position == maze.GoalCell.Position)
                    {
                        ReconstructPath(current);
                        return true;
                    }

                    if (current.Position != maze.StartCell.Position)
                    {
                        if (Sleep != 0)
                            pnMaze.Invalidate();
                        maze.SetCellValue(current.Position, CellValue.Visited);
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
                                if (neughbor.Position != maze.GoalCell.Position)
                                {
                                    maze.SetCellValue(neughbor.Position, CellValue.Neighbor);
                                    if (Sleep != 0)
                                        pnMaze.Invalidate();
                                }

                                openSet.Add(neughbor);

                                Thread.Sleep(Sleep);
                            }
                        }
                    }
                }
            }

            timer1.Stop();
            EnableControl();          
            MessageBox.Show("Không tìm thấy đường đi", "AStart Visualization", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (Maze.Cells[current.Position.I, current.Position.J + 1].Value != CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I, current.Position.J + 1]);

            if (current.Position.I - 1 >= 0)
                if (Maze.Cells[current.Position.I - 1, current.Position.J].Value != CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I - 1, current.Position.J]);

            if (current.Position.J - 1 >= 0)
                if (Maze.Cells[current.Position.I, current.Position.J - 1].Value != CellValue.Wall)
                    nei.Add(Maze.Cells[current.Position.I, current.Position.J - 1]);

            if (current.Position.I + 1 < Maze.MazeI)
                if (Maze.Cells[current.Position.I + 1, current.Position.J].Value != CellValue.Wall)
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

        #region Common

        private void EnableControl()
        {
            btnClearStep.Enabled = true;
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            tbrSleep.Enabled = true;
            btnClear.Enabled = true;
            txtCellSize.Enabled = true;
        }

        private void DisableControl()
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            btnFind.Enabled = false;
            btnStop.Enabled = true;
            tbrSleep.Enabled = false;
            btnClear.Enabled = false;
            btnClearStep.Enabled = false;
            txtCellSize.Enabled = false;
        }

        // dùng cho vòng lặp
        private CellPositon ConvertToIJ(Point location)
        {
            return new CellPositon { I = location.Y / CELL_SIZE, J = location.X / CELL_SIZE };
        }

        private void txtCellSize_KeyDown(object sender, KeyEventArgs e)
        {
            int cellSize;

            if (e.KeyCode == Keys.Enter && int.TryParse(txtCellSize.Text, out cellSize))
            {
                if (cellSize == 0 || cellSize == 1)
                {
                    MessageBox.Show("Chơi vậy lấy gì vẽ (╬▔皿▔)╯");
                    return;
                }
                else if (cellSize > 1 && cellSize < 10)
                {
                    MessageBox.Show("Eyyy, mắt sáng đó (⓿_⓿)");
                }

                CELL_SIZE = cellSize;
                InitMaze();
                pnMaze.Invalidate();
            }
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
