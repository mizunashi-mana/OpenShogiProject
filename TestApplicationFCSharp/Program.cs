﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationFCSharp
{
    using Board;
    class Program
    {
        static void Main(string[] args)
        {
            IOManager.printBoard(new BoardState(new NormalBoard()));
            Console.ReadLine();
        }
    }
}
