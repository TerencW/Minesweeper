using Minesweeper.Model;
using Minesweeper.Model.Mine;

namespace Minesweeper.Test
{
    public class MineGridTests
    {
        [Test]
        public void GetRightPosition()
        {
            var grid = new MineBoard(4, 0);

            var pos = grid.GetPostion("A1");
            Assert.Multiple(() =>
            {
                Assert.That(pos.Row, Is.EqualTo(0));
                Assert.That(pos.Column, Is.EqualTo(0));
            });
        }

      


    }
}
