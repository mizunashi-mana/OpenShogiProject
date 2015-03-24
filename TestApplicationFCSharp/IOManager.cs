using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationFCSharp
{
    using Board;

    class IOManager
    {
        public static readonly ReadOnlyDictionary<Komas, string> komaShows = 
            new ReadOnlyDictionary<Komas,string>(new Dictionary<Komas,string>
        {
            {Komas.NONE, "　"},
            {Komas.FUHYO, "歩"},
            {Komas.GINSHO, "銀"},
            {Komas.GYOKU, "玉"},
            {Komas.HISHA, "飛"},
            {Komas.KAKUGYO, "角"},
            {Komas.KEIMA, "桂"},
            {Komas.KINSHO, "金"},
            {Komas.KYOSHA, "香"},
            {Komas.NARIGIN, "全"},
            {Komas.NARIKEI, "圭"},
            {Komas.NARIKYO, "杏"},
            {Komas.OSHO, "王"},
            {Komas.RYUMA, "馬"},
            {Komas.RYUOU, "竜"},
            {Komas.TOKIN, "と"},
        });

        public static readonly ReadOnlyDictionary<PlayerTypes,ConsoleColor> pTypeColors =
            new ReadOnlyDictionary<PlayerTypes,ConsoleColor>(new Dictionary<PlayerTypes, ConsoleColor>
        {
            {PlayerTypes.NONE, ConsoleColor.White},
            {PlayerTypes.SENTE, ConsoleColor.White},
            {PlayerTypes.GOTE, ConsoleColor.Green},
        });

        public static void printBoard(BoardState bst)
        {
            int komaShowLength = 2;
            int barShowLength = bst.boardWidth * (1 + komaShowLength) + 1;

            // 盤面描写
            var sColor = Console.ForegroundColor;
            Console.ResetColor();
            Console.WriteLine(new string('-', barShowLength));
            for (int r = 0; r < bst.boardHeight; r++ )
            {
                for (int c = 0; c < bst.boardWidth; c++)
                {
                    Console.Write("|");
                    Console.ForegroundColor = pTypeColors[bst[r, c].pType];
                    Console.Write(komaShows[bst[r, c].Koma]);
                    Console.ResetColor();
                }
                Console.WriteLine("|");
                Console.WriteLine(new string('-', barShowLength));
            }
            Console.ForegroundColor = sColor;
        }
    }
}
