using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class Player
    {
        private int m_Score;
        private string m_Name;
        private PlayerType m_PlayerType;
        private char m_ShapeOfDisk;

        public enum PlayerType
        {
            Human = 1,

            Computer = 2
        }

        public Player(string i_Name, char i_Shape, PlayerType i_PlayerType)
        {
            m_Score = 2;
            m_Name = i_Name;
            m_ShapeOfDisk = i_Shape;
            m_PlayerType = i_PlayerType;
        }

        public string Name
        {
            get { return m_Name; }
        }

        public PlayerType TypeOfPlayer
        {
            get { return m_PlayerType; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public char Shape
        {
            get { return m_ShapeOfDisk; }
        }
    }
}
