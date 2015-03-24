using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationFCSharp
{
    public enum Komas
    {
        NONE,       // 無し
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
        NONE,   // 無し
        SENTE,  // 先手
        GOTE,   // 後手
    }

    namespace Board
    {

        public abstract class BaseBoard
        {
            public abstract int boardWidth { get; }
            public abstract int boardHeight { get; }

            public abstract KomaState[,] GetNowKomaStates();
        }

        public class NormalBoard : BaseBoard
        {
            private KomaState[,] __komaStates;

            public override int boardWidth
            {
                get
                {
                    return this.__komaStates.GetLength(1);
                }
            }

            public override int boardHeight
            {
                get
                {
                    return this.__komaStates.GetLength(0);
                }
            }

            public NormalBoard()
            {
                var komas = new Komas[9, 9]{
                    {Komas.KYOSHA, Komas.KEIMA, Komas.GINSHO, Komas.KINSHO, Komas.GYOKU, Komas.KINSHO, Komas.GINSHO, Komas.KEIMA, Komas.KYOSHA},
                    {Komas.NONE, Komas.HISHA, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.KAKUGYO, Komas.NONE},
                    {Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO},
                    {Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE},
                    {Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE},
                    {Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE},
                    {Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO, Komas.FUHYO},
                    {Komas.NONE, Komas.KAKUGYO, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.NONE, Komas.HISHA, Komas.NONE},
                    {Komas.KYOSHA, Komas.KEIMA, Komas.GINSHO, Komas.KINSHO, Komas.OSHO, Komas.KINSHO, Komas.GINSHO, Komas.KEIMA, Komas.KYOSHA},
                };

                this.__komaStates = new KomaState[9, 9];
                for (int r = 0, a = this.__komaStates.GetLength(0) / 2;
                    r < this.__komaStates.GetLength(0); r++)
                {
                    for (int c = 0; c < this.__komaStates.GetLength(1); c++)
                    {
                        var ptype = PlayerTypes.NONE;
                        if (komas[r, c] != Komas.NONE)
                        {
                            if (r < a) ptype = PlayerTypes.SENTE;
                            else ptype = PlayerTypes.GOTE;
                        }
                        this.__komaStates[r, c] = new KomaState(komas[r, c], ptype);
                    }
                }
            }

            public override KomaState[,] GetNowKomaStates()
            {
                var ret = new KomaState[this.boardHeight, this.boardWidth];
                Array.Copy(this.__komaStates, ret, this.__komaStates.Length);
                return ret;
            }
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

        public class BoardState : System.Collections.IEnumerable
        {
            private readonly KomaState[,] _komastate;

            public KomaState this[int row, int col]
            {
                get
                {
                    return this._komastate[row, col];
                }
            }

            public int boardWidth
            {
                get
                {
                    return this._komastate.GetLength(1);
                }
            }

            public int boardHeight
            {
                get
                {
                    return this._komastate.GetLength(0);
                }
            }

            public BoardState(BaseBoard board)
            {
                this._komastate = board.GetNowKomaStates();
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this._komastate.GetEnumerator();
            }

        }

    }
}
