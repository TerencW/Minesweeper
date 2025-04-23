using Minesweeper.Model;
using Minesweeper.Model.Mine;
using Minesweeper.Model.TikTacTok;

namespace Minesweeper
{
    public  class Game
    {
        public delegate void GameDelegate();

        public  void StartGame()
        {
            string input = "";
            while (input !="A" && input != "B")
            {
                Console.WriteLine($"Please Select a Game :");
                Console.WriteLine($"A: Minesweeper  B: Tik Tak Toe");
                input = Console.ReadLine()?.ToUpper() ?? "";
            }

            GameDelegate? selectedGame = input switch
            {
                "A" => new GameDelegate(MineSweeper),
                "B" => new GameDelegate(TikTakToe),
                _ => throw new NotImplementedException(),
            };
            selectedGame.Invoke();        
        }

        private static int ValidateIntInput()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid Input Integer.Please re-enter : ");

            }
            return result;
        }


        private void MineSweeper()
        {

            Console.WriteLine("Welcome to Minesweeper!");

            //Get Grid Size 
            Console.WriteLine("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");
            int gridSize = ValidateIntInput();

            //Get Numer of Mine 
            int maxMines = (int)(gridSize * gridSize * 0.35);
            Console.WriteLine($"Enter the number of mines to place on the grid (maximum is 35% of the total squares = {maxMines}): ");
            int mines = ValidateIntInput();
            while (mines > maxMines)
            {
                Console.WriteLine($"Exceed Max Mine!!");
                Console.WriteLine($"Re-Enter the number of mines to place on the grid (maximum is 35% of the total squares = {maxMines}): ");
                mines = ValidateIntInput();
            }


            //Initiate The Game  
            Console.WriteLine($"Generating Mine Grid ");
            var mineGrid = new MineBoard(gridSize, mines);


            //Dislay Grid 
            Console.WriteLine("Here is your minefield:");
            mineGrid.DisplayGrid();

            while (true)
            {
                Console.Write("Select a square to reveal (e.g. A1): ");
                string input = Console.ReadLine() ?? "";

                try
                {
                    Postion inputPosition = mineGrid.GetPostion(input);


                    //Check hit mine , if yes end game 
                    if (mineGrid.HitMine(inputPosition))
                    {
                        Console.WriteLine("Oh no, you detonated a mine! Game over.");
                        break;
                    }

                    // Uncover all adjacent squares until it reaches squares that do have adjacent mines
                    mineGrid.ShowCell(inputPosition);

                    //Check wining , if yes end game 
                    if (mineGrid.Win())
                    {
                        Console.WriteLine("Congratulations, you have won the game!");
                        break;
                    }

                    //Dislay Lastest Grid 
                    Console.WriteLine("Here is your updated minefield:");
                    mineGrid.DisplayGrid();
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            //Display The grid result 
            Console.WriteLine("Show All");
            mineGrid.ShowAll();

            Console.WriteLine("Press any key to play again...");
            Console.ReadKey();
            Console.Clear();
            StartGame();
        }

        private void TikTakToe()
        {

            Console.WriteLine($"Welcome to Tik Tak Toe");
            var tiktaktoe = new TikTakToeBoard(3);

            Console.WriteLine("Here is your Board:");
            tiktaktoe.DisplayGrid();

            while (true)
            {

                Console.Write("Select a field to place  (e.g. A1): ");
                string input = Console.ReadLine() ?? "";

                try
                {
                  
                    tiktaktoe.PlaceMark(input);
         

                    if (tiktaktoe.Win())
                    {
                        Console.WriteLine("Congratulations, you have won the game!");
                        break;
                    }

                    if (tiktaktoe.CompWin())
                    {
                        Console.WriteLine("You have lose the game!");
                        break;
                    }
                    if (tiktaktoe.IsFull())
                    {
                        Console.WriteLine("Equal");
                        break;
                    }

                    tiktaktoe.DisplayGrid();


                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
          
            }
            //Display The grid result 
            Console.WriteLine("Show All");

            tiktaktoe.DisplayGrid();


            Console.WriteLine("Press any key to play again...");
            Console.ReadKey();
            Console.Clear();
            StartGame();
        }
    }
}
