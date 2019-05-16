using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class Controller
    {
        private ConsoleUserInterface m_UI;
        private GameManager m_Game;

        public Controller(ConsoleUserInterface i_UI)
        {
            m_UI = i_UI;
            string firstPlayerName;
            string secondPlayerName;
            Player.PlayerType secondPlayerType;
            int boardSize;
            m_UI.GetInfoFromPlayer(out firstPlayerName, out secondPlayerName, out secondPlayerType);
            boardSize = m_UI.GetBoardSize();
            m_Game = new GameManager(firstPlayerName, secondPlayerName, secondPlayerType, boardSize);
        }

        public void RunGame()
        {
            while (!m_Game.IsGameOver)
            {
                m_UI.DrawBoard(m_Game.Board);
                MakeTurn();
            }
        }

        private void MakeTurn()
        {
            Cell inputCell;
            Player whoIsTheWinner;
            List<Cell> optionalCellsForInput = new List<Cell>(2);
            bool isOneMoreRound;
            m_Game.MakeListOfOptionalMoves(optionalCellsForInput);
            if (optionalCellsForInput.Count == 0)
            {
                if (m_Game.MovesStatus == GameManager.eMovesStatus.MovesForTwoOfThem)
                {
                    m_Game.MovesStatus = GameManager.eMovesStatus.MovesForOneOfThem;
                    m_Game.ChangeTurn();
                }
                else if (m_Game.MovesStatus == GameManager.eMovesStatus.MovesForOneOfThem)
                {
                    m_Game.MovesStatus = GameManager.eMovesStatus.NoMovesForTwoOfThem;
                    m_Game.IsGameOver = true;
                    whoIsTheWinner = m_Game.CalculateWinner();
                    m_UI.PrintsWhoIsTheWinner(whoIsTheWinner);
                    isOneMoreRound = m_UI.IsOneMoreRound();
                    System.Threading.Thread.Sleep(2000);
                    if (isOneMoreRound)
                    {
                        m_Game.ResetData();
                    }
                }
            }
            else
            {
                m_UI.PrintCurrentDetails(m_Game.Xplayer, m_Game.Oplayer, m_Game.CurrentPlayer);
                if (m_Game.CurrentPlayer.TypeOfPlayer == Player.PlayerType.Human)
                {
                    inputCell = m_UI.GetInputCell(m_Game.Board.Size, m_Game.Board);
                    if (inputCell == null)
                    {
                        m_Game.IsGameOver = true;
                    }

                    while (!m_Game.IsGameOver && !m_Game.IsAvailaibleCell(inputCell))
                    {
                        m_UI.PrintNotAvailableCellMessage();
                        inputCell = m_UI.GetInputCell(m_Game.Board.Size, m_Game.Board);
                        if (inputCell == null)
                        {
                            m_Game.IsGameOver = true;
                        }
                    }
                }
                else
                {
                    m_Game.DoComputerTurn(optionalCellsForInput);
                }

                m_Game.ChangeTurn();
            }
        }
    }
}
