namespace Minesweeper.BaseClass
{
    public class Cell
    {
        public bool Show { get; set; }

        public virtual string Display()
        {
            return Show ? "_": " ";
        }

    }
}
