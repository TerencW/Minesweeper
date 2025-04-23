using Minesweeper.BaseClass;

namespace Minesweeper.Model.TikTacTok
{
    class TikTakToeBoard : Board<TikTakToeCell>
    {
        public List<string> availableOption = ["A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3"];
        private readonly CellState _player = CellState.O;
        private readonly CellState _computer = CellState.X;

        public TikTakToeBoard(int size) : base(size)
        {
            GenerateGrid();
        }


        public override void ShowAll()
        {
            throw new NotImplementedException();
        }


        public void PlaceMark(string input )
        {
            Postion inputPosition = GetPostion(input);

            var row = inputPosition.Row;
            var col = inputPosition.Column;

            if (grid[row, col].TikCell != CellState.Empty) 
                throw new NotImplementedException(); 

            grid[row, col].TikCell = _player;

            availableOption.Remove(input);

            if ( availableOption.Count > 0)
            {
                Random rand = new();
                int index = rand.Next(availableOption.Count);
                string randomItem = availableOption[index];

                Console.WriteLine($"Comp Turn {randomItem}");
                Postion compPostion = GetPostion(randomItem);
                grid[compPostion.Row, compPostion.Column].TikCell = _computer;
                availableOption.Remove(randomItem);
            }
       
        }

        public override bool Win( )
        {
            return CheckOneLine(_player);
        }


     
        public bool CompWin()
        {
              return CheckOneLine(_computer);

        }

        public bool CheckOneLine(CellState symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((grid[i, 0].TikCell == symbol && grid[i, 1].TikCell == symbol && grid[i, 2].TikCell == symbol) ||
                  (grid[0, i].TikCell == symbol && grid[1, i].TikCell == symbol && grid[2, i].TikCell == symbol))
                    return true;
            }


            return (grid[0, 0].TikCell == symbol && grid[1, 1].TikCell == symbol && grid[2, 2].TikCell == symbol) ||
                   (grid[0, 2].TikCell == symbol && grid[1, 1].TikCell == symbol && grid[2, 0].TikCell == symbol);

        }


        public bool IsFull()
        {
            foreach (var TikCell in grid)
                if (TikCell.TikCell == CellState.Empty) return false;
            return true;
        }

    }
}
