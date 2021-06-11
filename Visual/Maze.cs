namespace Visual
{
    public class Maze
    {
        public readonly int MazeI, MazeJ;
        public Cell[,] Cells { get; set; }

        private CellPositon startPos;
        public CellPositon StartPosition
        {
            get => startPos;
            private set
            {
                if (startPos != null)
                {
                    Cells[startPos.I, startPos.J].Value = CellValue.None;
                }

                Cells[value.I, value.J].Value = CellValue.Start;
                startPos = value;
            }
        }

        private CellPositon goalPos;
        public CellPositon GoalPosition
        {
            get => goalPos;
            private set
            {
                if (goalPos != null)
                {
                    Cells[goalPos.I, goalPos.J].Value = CellValue.None;
                }
                Cells[value.I, value.J].Value = CellValue.Goal;
                goalPos = value;
            }
        }

        public void SetCellValue(CellPositon position, CellValue value)
        {
            if (startPos != null)
            {
                if (position.I == startPos.I && position.J == startPos.J && value != CellValue.Start)
                {
                    startPos = null;
                }
            }
            else if (goalPos != null)
            {
                if (position.I == goalPos.I && position.J == goalPos.J && value != CellValue.Goal)
                {
                    goalPos = null;
                }
            }

            if (value == CellValue.Start)
            {
                StartPosition = position;
            }
            else if (value == CellValue.Goal)
            {
                GoalPosition = position;
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
        public CellValue Value { get;internal set; }
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
            return obj is CellPositon b ? (I == b.I && J == b.J) : false;
        }

        public override int GetHashCode()
        {
            return (I, J).GetHashCode();
        }
    }
}
