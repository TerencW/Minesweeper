using Minesweeper.BaseClass;

namespace Minesweeper.Model.Mine
{
    public class MineBoard : Board<MineCell>
    {

        private readonly int _totalMines;
        private int revealedCount;
        private static readonly List<(int, int)> _nearByCell =
        [
            (-1,-1),  //Left Top 
            (-1,0),  // Left 
            (-1,1), // Left Botton
            (0,-1), // Top
            (0,1), // Bottom
            (1,-1), // Right Top 
            (1,0), // Right 
            (1,1) // Right Bottom 
        ];

        public MineBoard(int size, int mineCount) : base(size) 
        {
            _totalMines = mineCount;
            GenerateGrid();
            GenerateRandomMines();
            SetAdjacency();
        }



        /// <summary>
        /// Generate Randow Mine Within the Grid 
        /// </summary>
        private void GenerateRandomMines()
        {
            Random rand = new();
            int placed = 0;

            while (placed < _totalMines)
            {
                int row = rand.Next(_size);
                int column = rand.Next(_size);

                if (!grid[row, column].HasMine)
                {
                    grid[row, column].HasMine = true;
                    placed++;
                }
            }
        }

        /// <summary>
        /// Set the Adjacency 
        /// </summary>
        private void SetAdjacency()
        {
            for (int row = 0; row < _size; row++)
                for (int column = 0; column < _size; column++)
                {
                    //if have mine skip
                    if (grid[row, column].HasMine) continue;
                    int count = 0;

                    //loop the near by cell to check having mine 
                    foreach (var (left, right) in _nearByCell)
                    {
                        int nr = row + left, nc = column + right;
                        if (WithinGrid(nr, nc) && grid[nr, nc].HasMine)
                            count++;
                    }
                    grid[row, column].Adjacent = count;

                }
        }

        /// <summary>
        /// Check the position input has Mine , if yes return true BOOMMMM!
        /// </summary>
        /// <param name="postion"></param>
        /// <returns></returns>
        public bool HitMine(Postion postion) => grid[postion.Row, postion.Column].HasMine;

        /// <summary>
        /// Show the cell and nearby Cell if no mine hit 
        /// </summary>
        /// <param name="postion"></param>
        public void ShowCell(Postion postion)
        {
            Queue<Postion> queue = new();
            queue.Enqueue(postion);

            while (queue.Count > 0)
            {
                var currentPostion = queue.Dequeue();
                var currentCell = grid[currentPostion.Row, currentPostion.Column];
                if (!currentCell.Show)
                {
                    currentCell.Show = true;
                    revealedCount++;
                }

                foreach (var (dr, dc) in _nearByCell)
                {
                    int nr = currentPostion.Row + dr, nc = currentPostion.Column + dc;

                    if (!WithinGrid(nr, nc)) continue;

                    var neighbor = grid[nr, nc];

                    if (!neighbor.Show && !neighbor.HasMine)
                    {
                        neighbor.Show = true;
                        revealedCount++;
                        if (neighbor.IsEmpty)
                            queue.Enqueue(new Postion { Row = nr, Column = nc });
                    }
                }
            }
        }

        /// <summary>
        /// Show All the Grid and Display
        /// </summary>
        public override void ShowAll()
        {

            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                {
                    grid[i, j].Show = true;
                }
            DisplayGrid();
        }

        /// <summary>
        /// Display the Mine Grid
        /// </summary>
      

        /// <summary>
        /// Check the pass in postion is within the Grid Size
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        //private bool WithinGrid(int row, int column) => row >= 0 && row < _size && column >= 0 && column < _size;

        /// <summary>
        /// Check is Winning the Game 
        /// </summary>
        /// <returns></returns>
        public override bool Win() => revealedCount == _size * _size - _totalMines;

    
    }

}