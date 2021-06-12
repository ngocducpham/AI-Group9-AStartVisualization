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
                if (startCell != null)
                {
                    startCell.Value = CellValue.None;
                }

                startCell = value;
                if (startCell != null)
                    startCell.Value = CellValue.Start;
            }
        }

        private Cell goalCell;
        public Cell GoalCell
        {
            get => goalCell;
            set
            {
                if (goalCell != null)
                {
                    goalCell.Value = CellValue.None;
                }

                goalCell = value;
                if (goalCell != null)
                    goalCell.Value = CellValue.Goal;
            }
        }

        public void SetCellValue(CellPositon position, CellValue value)
        {
            // vị trí đích hoặc đầu bị ghi đè bằng giá trị khác
            if (startCell != null && position == startCell.Position && value != CellValue.Start)
            {
                StartCell = null;
            }
            else if (goalCell != null && position == goalCell.Position && value != CellValue.Goal)
            {
                GoalCell = null;
            }

            if (value == CellValue.Start)
            {
                StartCell = Cells[position.I, position.J];
            }
            else if (value == CellValue.Goal)
            {
                GoalCell = Cells[position.I, position.J];
            }

            Cells[position.I, position.J].Value = value;
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
                        Value = CellValue.None,
                        Position = new CellPositon { I = i, J = j }
                    };
                }
            }
        }

    }

    public class Cell
    {
        public CellValue Value { get; internal set; }
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

    public enum CellValue
    {
        None,
        Goal,
        Start,
        Wall,
        Neighbor,
        Visited,
        Path
    }

    public class CellPositon
    {
        public int I { get; set; }
        public int J { get; set; }

        public static bool operator ==(CellPositon a, CellPositon b)
        {
            if (a is null)
            {
                return b is null;
            }

            return a.Equals(b);
        }

        public static bool operator !=(CellPositon a, CellPositon b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is CellPositon b && (I == b.I && J == b.J);
        }

        public override int GetHashCode()
        {
            return I.GetHashCode() ^ J.GetHashCode();
        }
    }
}
