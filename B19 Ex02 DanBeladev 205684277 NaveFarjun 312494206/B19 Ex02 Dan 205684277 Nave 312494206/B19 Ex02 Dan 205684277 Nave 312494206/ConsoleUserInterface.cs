using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class ConsoleUserInterface
    {
        private const int k_NumOfCharacters = 2;

        public void DrawBoard(Board i_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            char currentChar = 'A';
            Console.Write("  ");

            for (int j = 0; j < i_Board.Size; j++)
            {
                Console.Write("  {0} ", currentChar++);
            }

            Console.WriteLine();
            Console.Write(" ");
            for (int i = 0; i < i_Board.Size; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < (i_Board.Size * 4) + 1; j++)
                {
                    Console.Write("=");
                }

                Console.Write(Environment.NewLine + (i + 1) + " ");
                for (int j = 0; j < i_Board.Size; j++)
                {
                    if (i_Board.GetCell(i, j).Content == ' ')
                    {
                        Console.Write("|   ");
                    }

                    if (i_Board.GetCell(i, j).Content == 'X')
                    {
                        Console.Write("| X ");
                    }

                    if (i_Board.GetCell(i, j).Content == 'O')
                    {
                        Console.Write("| O ");
                    }

                    if (j == i_Board.Size - 1)
                    {
                        Console.Write("|");
                    }
                }

                if (i == i_Board.Size - 1)
                {
                    Console.Write(Environment.NewLine + "  ");
                    for (int j = 0; j < (i_Board.Size * 4) + 1; j++)
                    {
                        Console.Write("=");
                    }
                }

                Console.Write(Environment.NewLine + " ");
            }
        }

        public void PrintsWhoIsTheWinner(Player i_whoIsTheWinner)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            string GameOverMessage;
            string whoIsTheWinner;
            if (i_whoIsTheWinner != null)
            {
                whoIsTheWinner = string.Format(
                    @"  The Winner Is: {0} with {1} points
===============================================================================",
i_whoIsTheWinner.Name,
i_whoIsTheWinner.Score);
            }
            else
            {
                whoIsTheWinner = "There is no Winner for this game";
            }

            GameOverMessage = string.Format(
@"===============================================================================
                             GAME OVER!

                           {0}",
whoIsTheWinner);
            Console.WriteLine(GameOverMessage);
        }

        public bool IsOneMoreRound()
        {
            string askForAnotherRound = "If you want to play another round press 'Y' else press 'N'";
            string restart;
            bool isOneMoreRound = true;
            Console.WriteLine(askForAnotherRound);
            restart = Console.ReadLine();

            while (restart != "Y" && restart == "y" && restart == "N" && restart == "n")
            {
                Console.WriteLine("Please Enter only 'Y' or 'N'");
                restart = Console.ReadLine();
            }

            if (restart == "N" || restart == "n")
            {
                isOneMoreRound = false;
            }

            return isOneMoreRound;
        }

        public Cell GetInputCell(int i_boardSize, Board i_board)
        {
            Cell chosenCell;
            string choice = Console.ReadLine();
            while (!IsLegalCellInput(choice, i_boardSize))
            {
                Console.WriteLine("FATAL ERROR, Your input is invalid");
                choice = Console.ReadLine();
            }

            if (IsQuit(choice))
            {
                chosenCell = null;
            }
            else
            {
                chosenCell = i_board.GetCell(choice[1] - '0' - 1, choice[0] - 'A');
            }

            return chosenCell;
        }

        private bool IsLegalCellInput(string i_Choice, int i_BoardSize)
        {
            return (
                IsInsideCollBounds(i_Choice, i_BoardSize) &&
                IsInsideRowsBounds(i_Choice, i_BoardSize) &&
                IsCorrectLength(i_Choice)
                ) || IsQuit(i_Choice);
        }

        private bool IsInsideCollBounds(string i_Choice, int i_BoardSize)
        {
            return i_Choice[0] >= 'A' && i_Choice[0] <= ('A' + i_BoardSize - 1);
        }

        private bool IsInsideRowsBounds(string i_Choice, int i_BoardSize)
        {
            return i_Choice[1] > '0' && i_Choice[1] <= '0' + i_BoardSize;
        }

        private bool IsCorrectLength(string i_Choice)
        {
            return i_Choice.Length == k_NumOfCharacters;
        }

        private bool IsQuit(string i_Choice)
        {
            return i_Choice == "q" || i_Choice == "Q";
        }

        public void PrintNotAvailableCellMessage()
        {
            Console.WriteLine("ERROR, You can't use this cell");
        }

        public void PrintCurrentDetails(Player i_Xplayer, Player i_Oplayer, Player i_CurrentPlayer)
        {
            string message;
            message = string.Format(
@"{0}'s Score: {1}

 {2}'s Score: {3}

 

 {4}'s turn({5}): ",
i_Xplayer.Name,
i_Xplayer.Score,
i_Oplayer.Name,
i_Oplayer.Score,
i_CurrentPlayer.Name,
i_CurrentPlayer.Shape);
            Console.WriteLine(message);
        }

        public void GetInfoFromPlayer(out string o_PlayerName, out string o_secondPlayerName, out Player.PlayerType o_secondPlayerType)
        {
            bool isLegalInput;
            int choice;
            Console.WriteLine("Enter first Name of player");
            o_PlayerName = Console.ReadLine();
            Console.WriteLine("Enter '1' for play against friend or '2' for play against computer");
            isLegalInput = int.TryParse(Console.ReadLine(), out choice);
            while ((choice != (int)Player.PlayerType.Human && choice != (int)Player.PlayerType.Computer) || (!isLegalInput))
            {
                Console.WriteLine("Error!, Enter '1' for play against friend or '2' for play against computer");
                isLegalInput = int.TryParse(Console.ReadLine(), out choice);
            }

            o_secondPlayerType = (Player.PlayerType)choice;
            if (o_secondPlayerType == Player.PlayerType.Human)
            {
                Console.WriteLine("Enter second Name of player");
                o_secondPlayerName = Console.ReadLine();
            }
            else
            {
                o_secondPlayerName = "Computer";
            }
        }

        public int GetBoardSize()
        {
            bool isLegalSize;
            int sizeOfBoard;
            Console.WriteLine("Enter '6' for 6x6 board game or '8' for 8x8 board game");
            isLegalSize = int.TryParse(Console.ReadLine(), out sizeOfBoard);
            while (((sizeOfBoard != 6) && (sizeOfBoard != 8) && (sizeOfBoard != 4)) || (!isLegalSize))
            {
                Console.WriteLine("ERROR, please Enter '6' for 6x6 board game or '8' for 8x8 board game");
                isLegalSize = int.TryParse(Console.ReadLine(), out sizeOfBoard);
            }

            return sizeOfBoard;
        }
    }
}
