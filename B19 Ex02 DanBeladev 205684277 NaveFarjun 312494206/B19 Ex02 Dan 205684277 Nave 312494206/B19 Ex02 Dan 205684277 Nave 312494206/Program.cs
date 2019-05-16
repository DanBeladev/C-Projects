using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B19_Ex02_Dan_205684277_Nave_312494206
{
    public class Program
    {
        // TODO:: STYLE COP AND TXT FILE IN SOLUTION
        public static void Main()
        {
            ConsoleUserInterface UI = new ConsoleUserInterface();
            Controller gameController = new Controller(UI);
            gameController.RunGame();
        }
    }
}
