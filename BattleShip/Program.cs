using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Program
    {   
        public static int shipCount = 5;
        static void Main(string[] args)
        {
            bool gameWon = false;
            int playerTurn = 1;

            //ask user how many ships between 1 and 9
            shipCount = ConvertInput("How many ships would you like to play with (1-9): ", 9);

            int playerOneShips = shipCount;
            int playerTwoShips = shipCount;

            string[,] playerOneBoard = new string[shipCount + 1, shipCount + 1];
            string[,] playerTwoBoard = new string[shipCount + 1, shipCount + 1];

            string[,] playerOneGuesses = new string[shipCount + 1, shipCount + 1];
            string[,] playerTwoGuesses = new string[shipCount + 1, shipCount + 1];

            PopulateBoard(playerOneBoard);
            PopulateBoard(playerTwoBoard);

            PopulateBoard(playerOneGuesses);
            PopulateBoard(playerTwoGuesses);

            //player 1 ship assignment
            for (int i = 0; i < shipCount; i++)
            {
                PrintBoard(playerOneBoard);

                int x = ConvertInput("Please enter the x value of your ship player 1: ", shipCount);
                int y = ConvertInput("Please enter the y value of your ship player 1: ", shipCount);

                AssignShip(playerOneBoard, x, y);
            }

            //player 2 ship assignment
            for (int i = 0; i < shipCount; i++)
            {
                PrintBoard(playerTwoBoard);

                int x = ConvertInput("Please enter the x value of your ship player 2: ", shipCount);
                int y = ConvertInput("Please enter the y value of your ship player 2: ", shipCount);

                AssignShip(playerTwoBoard, x, y);
            }


            //game loop
            do
            {
                if (playerTurn == 1)
                {
                    PrintBoard(playerOneGuesses);
                    int x = ConvertInput("Please enter the x value of your guess player 1: ", shipCount);
                    int y = ConvertInput("Please enter the y value of your guess player 1: ", shipCount);

                    playerTwoShips += Guess(playerOneGuesses, playerTwoBoard, x, y);

                    PrintBoard(playerOneGuesses);

                    if (playerTwoShips == 0)
                    {
                        Console.WriteLine("Player 1 wins!");
                        gameWon = true;
                    }
                    else 
                    {
                        GetUserInput("Press enter to change turns.");
                    }              

                    playerTurn = 2;
                }
                else 
                { 
                    PrintBoard(playerTwoGuesses);

                    int x = ConvertInput("Please enter the x value of your guess player 2: ", shipCount);
                    int y = ConvertInput("Please enter the y value of your guess player 2: ", shipCount);

                    playerOneShips += Guess(playerTwoGuesses, playerOneBoard, x, y);

                    PrintBoard(playerTwoGuesses);

                    if (playerOneShips == 0)
                    {
                        Console.WriteLine("Player 2 wins!");
                        gameWon = true;
                    }
                    else 
                    {
                        GetUserInput("Press enter to change turns.");
                    }

                    playerTurn = 1;
                }

            } while (gameWon != true);

            Console.ReadLine();
        }

        //takes real board and assigns a ship location
        public static void AssignShip(string[,] board, int x, int y)
        {
            board[y, x] = "S";
        }

        // validates and captures user coordinate input after displaying a message
        public static int ConvertInput(string message, int max)
        {
            bool isValidInput = false;
            int x = 0;
            do
            {
                Console.Write(message);

                string input = Console.ReadLine();

                isValidInput = Int32.TryParse(input, out x);

                //ensure valid number is actually valid in coordinates
                if (x < 1 || x > max) 
                {
                    isValidInput = false;
                }
            } while (!isValidInput);

            return x;
        }

        //displays a message to the user and then returns console response
        public static string GetUserInput(string message) 
        {
            Console.Write(message);

            string input = Console.ReadLine();

            Console.WriteLine();

            return input;
        }

        // if match on real board, mark with a hit. otherwise mark with a miss
        public static int Guess(string[,] guessBoard, string[,] realBoard, int x, int y) 
        {
            if (realBoard[y, x] == "S")
            {
                guessBoard[y, x] = "X";
                return -1;
            }
            else 
            {
                guessBoard[y, x] = "-";
                return 0;
            }
               
        }

        public static void PopulateBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++) 
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    //print blank on 0,0
                    if (i == 0 && j == 0)
                    {
                        board[i, j] = " ";
                    }
                    //first row coordinates
                    else if (i == 0)
                    {
                        board[i, j] = j.ToString();
                    }
                    //first col coordinates
                    else if (j == 0)
                    {
                        board[i, j] = i.ToString();
                    }
                    //unmarked battleship spaces
                    else
                    {
                        board[i, j] = "O";
                    }
                }
            }
        }

        public static void PrintBoard(string[,] board)
        {
            Console.Clear();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i,j]);
                }
                Console.WriteLine();
            }
        }


    }
}
