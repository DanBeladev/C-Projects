using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class Cell
    {
        private int m_row;
        private int m_col;
        private char m_content;

        public Cell(int i_row, int i_col, char i_content = ' ')
        {
            m_row = i_row;
            m_col = i_col;
            m_content = i_content;
        }

        public char Content
        {
            get { return m_content; }

            set { m_content = value; }
        }

        public int Row
        {
            get { return m_row; }
            set { m_row = value; }
        }

        public int Col
        {
            get { return m_col; }
            set { m_col = value; }
        }
    }
}
