using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class GameManager
    {
        private const int k_NumOfDirections = 8;
        private Board m_BoardGame;
        private Player m_XPlayer;
        private Player m_OPlayer;
        private Player m_curentTurn;
        private bool m_IsGameOver = false;
        private eMovesStatus m_StatusMoves = eMovesStatus.MovesForTwoOfThem;

        public enum eMovesStatus
        {
            NoMovesForTwoOfThem, MovesForOneOfThem, MovesForTwoOfThem
        }

        private void GetDirection(out int o_directionForRow, out int o_directionForColl, int i)
        {
            o_directionForRow = 0;
            o_directionForColl = 0;
            if (i == 0 || i == 1 || i == 2)
            {
                o_directionForRow = -1;
            }

            if (i == 0 || i == 3 || i == 5)
            {
                o_directionForColl = -1;
            }

            if (i == 5 || i == 6 || i == 7)
            {
                o_directionForRow = 1;
            }

            if (i == 2 || i == 4 || i == 7)
            {
                o_directionForColl = 1;
            }
        }

        public GameManager(string i_firstPlayerName, string i_secondPlayerName, Player.PlayerType i_secondPlayerType, int i_BoardSize)
        {
            m_XPlayer = new Player(i_firstPlayerName, 'X', Player.PlayerType.Human);
            m_OPlayer = new Player(i_secondPlayerName, 'O', i_secondPlayerType);
            m_BoardGame = new Board(i_BoardSize);
            m_curentTurn = m_XPlayer;
        }

        public void DoComputerTurn(List<Cell> i_OptionalCellsForInput)
        {
            System.Threading.Thread.Sleep(1000);
            Random randomCell = new Random();
            int cellIndex = randomCell.Next(0, i_OptionalCellsForInput.Count);
            PutDisk(m_curentTurn, i_OptionalCellsForInput[cellIndex]);
        }

        public bool MakeListOfOptionalMoves(List<Cell> i_optionalCellsForInput)
        {
            Cell currentCell;
            int directionForColl;
            int directionForRow;
            int recDepth = 0;
            bool res = false;
            for (int i = 0; i < m_BoardGame.Size; i++)
            {
                for (int j = 0; j < m_BoardGame.Size; j++)
                {
                    currentCell = m_BoardGame.GetCell(i, j);
                    if (currentCell.Content == ' ')
                    {
                        for (int k = 0; k < k_NumOfDirections; k++)
                        {
                            recDepth = 0;
                            GetDirection(out directionForRow, out directionForColl, k);
                            res = IsOptionalCell(currentCell, m_curentTurn, directionForRow, directionForColl, ref recDepth);
                            if (res)
                            {
                                i_optionalCellsForInput.Add(currentCell);
                                break;
                            }
                        }
                    }
                }
            }

            return res;
        }

        public void ChangeTurn()
        {
            if (m_curentTurn == m_XPlayer)
            {
                m_curentTurn = m_OPlayer;
            }
            else
            {
                m_curentTurn = m_XPlayer;
            }
        }

        private bool PutDisk(Player i_PlayerTurn, Cell i_To)
        {
            bool isLegalMove = false;
            int directionForColl;
            int directionForRow;
            int recDepth = 0;
            for (int i = 0; i < k_NumOfDirections; i++)
            {
                recDepth = 0;
                GetDirection(out directionForRow, out directionForColl, i);
                if (CalculateMove(i_To, i_PlayerTurn, directionForRow, directionForColl, ref recDepth))
                {
                    isLegalMove = true;
                }
            }

            if (isLegalMove)
            {
                i_PlayerTurn.Score++;
            }

            return isLegalMove;
        }

        private bool CalculateMove(Cell i_currentCell, Player i_PlayerTurn, int i_directionForRow, int i_directionForColl, ref int io_recDepth)
        {
            bool res = false;
            int neighborRow = i_currentCell.Row + i_directionForRow;
            int neighborCol = i_currentCell.Col + i_directionForColl;
            if ((neighborRow >= 0 && neighborRow < m_BoardGame.Size) &&
                (neighborCol >= 0 && neighborCol < m_BoardGame.Size))
            {
                Cell neighborCell = m_BoardGame.GetCell(neighborRow, neighborCol);
                if (io_recDepth != 0 && neighborCell.Content == i_PlayerTurn.Shape)
                {
                    ChangeDiskAndUpdateScore(i_currentCell, i_PlayerTurn, io_recDepth);
                    return true;
                }

                if (neighborCell.Content == ' ' || (io_recDepth == 0 && neighborCell.Content == i_PlayerTurn.Shape))
                {
                    return false;
                }
                else
                {
                    io_recDepth++;
                    res = CalculateMove(neighborCell, i_PlayerTurn, i_directionForRow, i_directionForColl, ref io_recDepth);
                    io_recDepth--;
                    if (res == true)
                    {
                        ChangeDiskAndUpdateScore(i_currentCell, i_PlayerTurn, io_recDepth);
                    }
                }
            }

            return res;
        }

        private void ChangeDiskAndUpdateScore(Cell i_currentCell, Player i_PlayerTurn, int i_recDepth)
        {
            i_currentCell.Content = i_PlayerTurn.Shape;
            if (i_recDepth != 0)
            {
                if (i_PlayerTurn == m_XPlayer)
                {
                    m_OPlayer.Score--;
                }
                else
                {
                    m_XPlayer.Score--;
                }

                i_PlayerTurn.Score++;
            }
        }

        private bool IsOptionalCell(Cell i_currentCell, Player i_PlayerTurn, int i_directionForRow, int i_directionForColl, ref int io_recDepth)
        {
            bool res = false;
            int neighborRow = i_currentCell.Row + i_directionForRow;
            int neighborCol = i_currentCell.Col + i_directionForColl;
            if ((neighborRow >= 0 && neighborRow < m_BoardGame.Size) &&
                (neighborCol >= 0 && neighborCol < m_BoardGame.Size))
            {
                Cell neighborCell = m_BoardGame.GetCell(neighborRow, neighborCol);
                if (io_recDepth != 0 && neighborCell.Content == i_PlayerTurn.Shape)
                {
                    return true;
                }

                if (neighborCell.Content == ' ' || (io_recDepth == 0 && neighborCell.Content == i_PlayerTurn.Shape))
                {
                    return false;
                }
                else
                {
                    io_recDepth++;
                    res = IsOptionalCell(neighborCell, i_PlayerTurn, i_directionForRow, i_directionForColl, ref io_recDepth);
                }
            }

            return res;
        }

        public Player CalculateWinner()
        {
            if (m_XPlayer.Score > m_OPlayer.Score)
            {
                return m_XPlayer;
            }
            else if (m_XPlayer.Score < m_OPlayer.Score)
            {
                return m_OPlayer;
            }
            else
            {
                return null;
            }
        }

        public void ResetData()
        {
            int boardSize = m_BoardGame.Size;
            m_BoardGame.InitBoard();
            m_XPlayer.Score = 2;
            m_OPlayer.Score = 2;
            m_curentTurn = m_XPlayer;
            m_IsGameOver = false;
            m_StatusMoves = eMovesStatus.MovesForTwoOfThem;
        }

        public eMovesStatus MovesStatus
        {
            get { return m_StatusMoves; }
            set { m_StatusMoves = value; }
        }

        public Board Board
        {
            get { return m_BoardGame; }
        }

        public bool IsAvailaibleCell(Cell i_InputCell)
        {
            bool res = false;
            if (i_InputCell.Content == ' ' && PutDisk(m_curentTurn, i_InputCell))
            {
                res = true;
            }

            return res;
        }

        public Player Xplayer
        {
            get { return m_XPlayer; }
        }

        public Player Oplayer
        {
            get { return m_OPlayer; }
        }

        public Player CurrentPlayer
        {
            get { return m_curentTurn; }
        }

        public bool IsGameOver
        {
            get { return m_IsGameOver; }
            set { m_IsGameOver = value; }
        }
    }
}
