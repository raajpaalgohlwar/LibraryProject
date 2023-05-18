//Raajpaal Gohlwar CIS 340 8:35 AM MiniProject2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MiniProject2
{
    class Utilities
    {
        //Prints two WriteLine's to the screen
        public void Pause()
        {
            WriteLine();
            WriteLine();
        }

        //Prints "Press any key to continue..."
        public void Break()
        {
            Write("Press any key to continue...");
            ReadKey();
        }

        //Invalid Number
        public void Invalid()
        {
            WriteLine("Invalid Number");
            WriteLine();
        }
    }
}
