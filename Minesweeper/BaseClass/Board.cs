using Minesweeper.Model;

namespace Minesweeper.BaseClass
{
    public abstract class Board<TCell> where TCell : Cell , new()
    {
        protected int _size;
        protected TCell[,] grid;
        protected Dictionary<string, Postion> mineList = [];

        public Board(int size)
        {
            _size = size;
            grid = new TCell[_size, _size];
        }

        protected bool WithinGrid(int row, int column) 
                => row >= 0 && row < _size && column >= 0 && column < _size;


        /// <summary>
        /// Generate Grid Row and Column and Add Position of Grid and checking value)
        /// </summary>
        protected void GenerateGrid()
        {
            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                {
                    grid[i, j] = new TCell();
                    char startChar = 'A';
                    char currentChar = (char)(startChar + i);
                    mineList.Add($"{currentChar}{j + 1}", new Postion { Row = i, Column = j });
                }
        }

        /// <summary>
        /// Get the Postion of the grid 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Postion GetPostion(string input)
        {
            if (mineList.TryGetValue(input, out var postion))
                if (postion != null)
                    return postion;
            throw new Exception();
        }

        public void DisplayGrid()
        {
            //display header 
            Console.Write("  ");
            for (int column = 1; column <= _size; column++)
                Console.Write($"{column} ");
            Console.WriteLine();

            char startChar = 'A';
            //display rows
            for (int row = 0; row < _size; row++)
            {
                char currentChar = (char)(startChar + row);
                Console.Write($"{currentChar} ");

                //display column
                for (int column = 0; column < _size; column++)
                    Console.Write($"{grid[row, column].Display()} ");
                Console.WriteLine();
            }
        }


        public abstract void ShowAll();
        public abstract bool Win();

      //  public abstract void DisplayGrid();

    }
}
