using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual
{
    public class Maze
    {
        public readonly int MazeI, MazeJ;
        public Cell[,] Cells { get; set; }

        private Cell startCell;
        public Cell StartCell 
        {
            get => startCell;
            set
            {
                if(startCell != null)
                {
                    startCell.Value = 0;
                }
                startCell = value;
                if (startCell == null)
                    return;
                startCell.Value = 2;
            }
        }

        private Cell goalCell;
        public Cell GoalCell 
        {
            get => goalCell;
            set
            {
                if(goalCell != null)
                {
                    goalCell.Value = 0;
                }

                goalCell = value;
                if (goalCell == null)
                    return;
                goalCell.Value = 3;
            }
        }

        public Maze(int width, int height)
        {
            // 30, 24
            Cells = new Cell[height, width];
            MazeI = height;
            MazeJ = width;

            InitData();
        }

        private void InitData()
        {
            for (int i = 0; i < MazeI; i++)
            {
                for (int j = 0; j < MazeJ; j++)
                {
                    Cells[i, j] = new Cell()
                    {
                        Value = 0,
                        Position = new CellPositon { I = i, J = j}
                        
                    };
                }
            }
        }

    }

    public class Cell
    {
        public int Value { get; set; }
        // điểm g
        public int gScore { get; set; }
        // f= g + h
        public double hScore { get; set; }
        public double fScore { get; set; }
        public CellPositon Position { get; set; }
        //public List<Cell> Neighbors { get;set; }
        public Cell CameFrom { get; set; }

        public Cell()
        {
            Position = new CellPositon();
            //Neighbors = new List<Cell>();
        }

    }

    public class CellPositon
    {
        public int I { get; set; }
        public int J { get; set; }
    }
}
