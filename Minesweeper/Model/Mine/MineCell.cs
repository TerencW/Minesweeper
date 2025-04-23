using Minesweeper.BaseClass;

namespace Minesweeper.Model.Mine
{
    public class MineCell : Cell
    {
        public bool HasMine { get; set; }

        public int Adjacent { get; set; }

        public bool IsEmpty => !HasMine && Adjacent == 0;

        public override string Display()
        {
            return Show
                ? HasMine ? "*" : Adjacent.ToString()
                : "_";
        }
    }
}