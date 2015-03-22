using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationFCSharp
{
    public class Board
    {
        public KomaState[,] komaStates;

        public Board()
        {

        }
    }

    public enum Komas
    {
        OSHO,       // 王将
        GYOKU,      // 玉将
        HISHA,      // 飛車
        RYUOU,      // 龍王
        KAKUGYO,    // 角行
        RYUMA,      // 龍馬
        KINSHO,     // 金将
        GINSHO,     // 銀将
        NARIGIN,    // 成銀
        KEIMA,      // 桂馬
        NARIKEI,    // 成桂
        KYOSHA,     // 香車
        NARIKYO,    // 成香
        FUHYO,      // 歩兵
        TOKIN,      // と金
    }

    public enum PlayerTypes
    {
        SENTE,  // 先手
        GOTE,   // 後手
    }
    
    public class KomaState
    {
        private readonly Komas _koma;
        public Komas Koma { get { return _koma; } }

        private readonly PlayerTypes _ptype;
        public PlayerTypes pType { get { return _ptype; } }

        public KomaState(Komas koma, PlayerTypes ptype)
        {
            this._koma = koma;
            this._ptype = ptype;
        }

    }

    public class BoardState
    {
        private readonly KomaState[,] _komastate;

        public KomaState this[int row, int col]{
            get
            {
                return this._komastate[row, col];
            }
        }

        public int boardWidth
        {
            get
            {
                return this._komastate.GetLength(0);
            }
        }

        public int boardHeight
        {
            get
            {
                return this._komastate.GetLength(1);
            }
        }

        public BoardState(Board board)
        {
            _komastate = null;
            Array.Copy(board.komaStates, this._komastate, board.komaStates.Length);
        }
    }
}
