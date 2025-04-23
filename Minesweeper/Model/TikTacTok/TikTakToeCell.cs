using Minesweeper.BaseClass;

namespace Minesweeper.Model.TikTacTok
{
    class TikTakToeCell : Cell
    {
        public CellState TikCell { get; set; } = CellState.Empty;

        public override string Display()
        {
            return TikCell != CellState.Empty ? TikCell.ToString() : "_";
        }

    }
}
