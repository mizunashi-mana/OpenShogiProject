using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationFCSharp
{
    using PlayerIDs = UInt32;

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
            protected abstract KomaState[,] komaStates
            {
                get;
            } 

            public int boardWidth { 
                get{
                    return this.komaStates.GetLength(1);
                } 
            }
            public int boardHeight { 
                get{
                    return this.komaStates.GetLength(0);
                } 
            }

            public KomaState[,] GetNowKomaStates()
            {
                var ret = new KomaState[this.boardHeight, this.boardWidth];
                Array.Copy(this.komaStates, ret, this.komaStates.Length);
                return ret;
            }

            public abstract PlayerTypes[] GetPlayers();



            public bool IsBoardSizeOver(KomaPoint kp) 
            {
                return 0 <= kp.r && kp.r < this.boardHeight && 0 <= kp.c && kp.c < this.boardWidth;
            }

            public abstract void ActionKomaMove(KomaPoint kp_pre, KomaPoint kp_nxt, PlayerTypes ptype);

            public abstract void ActionKomaPut(Komas koma, PlayerTypes ptypes, KomaPoint kp);

            public void ActionKomaMove(KomaPoint kp_pre, KomaPoint kp_nxt)
            {
                if (!IsBoardSizeOver(kp_pre) || !IsBoardSizeOver(kp_nxt)) throw new InvalidOperationException("指定された駒の位置がボードの範囲を超えています。");
                if (this.komaStates[kp_pre.r, kp_pre.c].Koma == Komas.NONE) throw new InvalidOperationException("指定された動かす駒の位置に駒が存在しません。");

            }

            public void ActionKomaPut(Komas koma, PlayerTypes ptype, KomaPoint kp)
            {

            }

        }

        public class NormalBoard : BaseBoard
        {
            protected readonly KomaState[,] __komaStates;
            protected override KomaState[,] komaStates
            {
                get { return this.__komaStates; }
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
        }

        public struct KomaState
        {
            public readonly Komas Koma;

            public readonly PlayerTypes pType;

            public KomaState(Komas koma, PlayerTypes ptype)
            {
                this.Koma = koma;
                this.pType = ptype;
            }

        }

        public struct KomaPoint
        {
            public uint row;
            public uint column;

            public uint r
            {
                get { return this.row; }
                set { this.row = value; }
            }

            public uint c
            {
                get { return this.column; }
                set { this.column = value; }
            }

            public uint col
            {
                get { return this.column; }
                set { this.column = value; }
            }

            public KomaPoint(uint row, uint col)
            {
                this.row = row;
                this.column = col;
            }
        }

        public class BoardState : System.Collections.IEnumerable
        {
            private readonly KomaState[,] _komastate;
            private readonly ReadOnlyDictionary<PlayerIDs, Komas> bringKomaStates;

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
