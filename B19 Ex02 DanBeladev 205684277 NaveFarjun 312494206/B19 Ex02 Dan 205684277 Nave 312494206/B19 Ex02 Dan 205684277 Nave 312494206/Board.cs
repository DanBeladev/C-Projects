using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class Board
    {
        private int m_SizeMatrix;

        private Cell[,] m_BoardMatrix;

        public Board(int i_SizeOfBoard)
        {
            m_SizeMatrix = i_SizeOfBoard;
            m_BoardMatrix = new Cell[i_SizeOfBoard, i_SizeOfBoard];
            InitBoard();
        }

        public void InitBoard()
        {
            for (int i = 0; i < m_SizeMatrix; i++)
            {
                for (int j = 0; j < m_SizeMatrix; j++)
                {
                    m_BoardMatrix[i, j] = new Cell(i, j, ' ');
                }
            }

            m_BoardMatrix[(m_SizeMatrix / 2) - 1, (m_SizeMatrix / 2) - 1].Content = 'O';
            m_BoardMatrix[(m_SizeMatrix / 2) - 1, (m_SizeMatrix / 2)].Content = 'X';
            m_BoardMatrix[(m_SizeMatrix / 2), (m_SizeMatrix / 2) - 1].Content = 'X';
            m_BoardMatrix[(m_SizeMatrix / 2), (m_SizeMatrix / 2)].Content = 'O';
        }

        public Cell GetCell(int i_Row, int i_Col)
        {
            return m_BoardMatrix[i_Row, i_Col];
        }

        public int Size
        {
            get { return m_SizeMatrix; }
        }
    }
}
