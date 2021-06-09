﻿using System;
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

        private void pnMaze_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);

        }

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


        private void pnMaze_MouseDown(object sender, MouseEventArgs e)
        {
            var cellPos = ConvertToIJ(e.Location);
            switch (DrawMode)
            {
                case DrawMode.DrawWall:
                    Maze.Cells[cellPos.I, cellPos.J].Value = 1;
                    BrushCell(cellPos.I, cellPos.J, Brushes.Black);
                    break;
                case DrawMode.DrawStart:
                    if (Maze.StartCell != null)
                        BrushCell(Maze.StartCell.Position.I, Maze.StartCell.Position.J, Brushes.White);
                    Maze.StartCell = Maze.Cells[cellPos.I, cellPos.J];
                    BrushCell(cellPos.I, cellPos.J, Brushes.SpringGreen);
                    break;
                case DrawMode.DrawGoal:
                    if (Maze.GoalCell != null)
                        BrushCell(Maze.GoalCell.Position.I, Maze.GoalCell.Position.J, Brushes.White);
                    Maze.GoalCell = Maze.Cells[cellPos.I, cellPos.J];
                    BrushCell(cellPos.I, cellPos.J, Brushes.Red);
                    break;
                case DrawMode.Delete:
                    Maze.Cells[cellPos.I, cellPos.J].Value = 0;
                    BrushCell(cellPos.I, cellPos.J, Brushes.White);
                    break;
                default:
                    break;
            }
            //BrushCell(MazeGraphics);
            // gọi sự kiện vẽ
            //pnMaze.Invalidate();
        }

        // vẽ tường
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

                Maze.Cells[cellPos.I, cellPos.J].Value = 1;
                BrushCell(cellPos.I, cellPos.J, Brushes.Black);
            }
            else if (DrawMode == DrawMode.Delete)
            {
                if (Maze.Cells[cellPos.I, cellPos.J] == Maze.StartCell)
                    Maze.StartCell = null;
                else if (Maze.Cells[cellPos.I, cellPos.J] == Maze.GoalCell)
                    Maze.GoalCell = null;
                Maze.Cells[cellPos.I, cellPos.J].Value = 0;
                BrushCell(cellPos.I, cellPos.J, Brushes.White);
            }


            //pnMaze.Invalidate();
        }

        private void pnMaze_MouseUp(object sender, MouseEventArgs e)
        {
            //DrawGrid(MazeGraphics);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            RefreshMaze();
            ThreadFind = new Thread(() => aStart(Maze, heur));
            ThreadFind.IsBackground = true;
            ThreadFind.Start();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            btnFind.Enabled = false;
            btnStop.Enabled = true;
        }

        private void RefreshMaze()
        {
            for (int i = 0; i < Maze.MazeI; i++)
            {
                for (int j = 0; j < Maze.MazeJ; j++)
                {
                    if (Maze.Cells[i, j].Value == 0)
                        BrushCell(i, j, Brushes.White);
                }
            }
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

        #region Algorithm

        private double heuristic1(Cell current)
        {
            return Math.Abs(current.Position.I - Maze.GoalCell.Position.I) + Math.Abs(current.Position.J - Maze.GoalCell.Position.J);
        }

        private double heuristic2(Cell current)
        {
            Point posCur = ConvertToXY(current.Position.I, current.Position.J);
            Point posGoal = ConvertToXY(Maze.GoalCell.Position.I, Maze.GoalCell.Position.J);

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
            Point posGoal = ConvertToXY(Maze.GoalCell.Position.I, Maze.GoalCell.Position.J);
            double dx = Math.Abs(posCur.X - posGoal.X);
            double dy = Math.Abs(posCur.Y - posGoal.Y);

            return (dy + dx) + (Math.Sqrt(2) - 2) * Math.Min(dy, dx);
        }

        private void reconstruct_path(Cell current)
        {
            while (true)
            {
                current = current.CameFrom;
                if (current == Maze.StartCell)
                    break;
                //current.Value = 6;
                BrushCell(current.Position.I, current.Position.J, Brushes.Yellow);
                Thread.Sleep(80);
            }

            DrawGrid(MazeGraphics);
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            //pnMaze.Invalidate();
        }

        private bool aStart(Maze maze, Heuristic her)
        {
            List<Cell> openSet = new List<Cell>();
            openSet.Add(maze.StartCell);

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
                    if (current == maze.GoalCell)
                    {

                        BrushCell(current.Position.I, current.Position.J, Brushes.Red);
                        reconstruct_path(current);
                        return true;
                    }

                    if (current != maze.StartCell)
                        BrushCell(current.Position.I, current.Position.J, Brushes.Pink);
                    Thread.Sleep(50);

                    openSet.Remove(current);

                    foreach (var neughbor in FindNeighbor(current))
                    {
                        var tentative_gScore = current.gScore + 1;
                        if (tentative_gScore < neughbor.gScore)
                        {
                            neughbor.CameFrom = current;
                            neughbor.gScore = tentative_gScore;
                            neughbor.fScore = neughbor.gScore + her(neughbor);
                            if (!Exists(openSet, neughbor))
                            {
                                if (neughbor != maze.StartCell)
                                    BrushCell(neughbor.Position.I, neughbor.Position.J, Brushes.Aqua);

                                openSet.Add(neughbor);
                            }
                        }

                    }
                }
            }
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
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

        private List<Cell> FindNeighbor(Cell current)
        {
            List<Cell> nei = new List<Cell>();

            if (current.Position.J + 1 < Maze.MazeJ && Maze.Cells[current.Position.I, current.Position.J + 1].Value != 1
                && Maze.Cells[current.Position.I, current.Position.J + 1].Value != 2)
                nei.Add(Maze.Cells[current.Position.I, current.Position.J + 1]);

            if (current.Position.I - 1 >= 0 && Maze.Cells[current.Position.I - 1, current.Position.J].Value != 1
                && Maze.Cells[current.Position.I - 1, current.Position.J].Value != 2)
                nei.Add(Maze.Cells[current.Position.I - 1, current.Position.J]);

            if (current.Position.J - 1 >= 0 && Maze.Cells[current.Position.I, current.Position.J - 1].Value != 1
                && Maze.Cells[current.Position.I, current.Position.J - 1].Value != 2)
                nei.Add(Maze.Cells[current.Position.I, current.Position.J - 1]);

            if (current.Position.I + 1 < Maze.MazeI && Maze.Cells[current.Position.I + 1, current.Position.J].Value != 1
                && Maze.Cells[current.Position.I + 1, current.Position.J].Value != 2)
                nei.Add(Maze.Cells[current.Position.I + 1, current.Position.J]);

            return nei;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

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
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            btnFind.Enabled = true;
            btnStop.Enabled = false;
            ThreadFind.Abort();
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
    }

    public enum DrawMode
    {
        DrawWall,
        DrawStart,
        DrawGoal,
        Delete
    }
}
