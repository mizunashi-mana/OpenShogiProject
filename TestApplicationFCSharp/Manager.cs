using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationFCSharp
{
    using Board;

    public enum BoardMode
    {
        NORMAL,
    }


    // IOManager部分をインターフェース化して、
    // GUI、CUIその他の形式に対応する予定
    // テストアプリ段階では、大丈夫っしょ
    public class GameManager
    {
        private readonly BaseBoard bBoard;

        public GameManager(BoardMode bmode = BoardMode.NORMAL)
        {
            bBoard = new NormalBoard();
            initialized();
        }

        private void initialized()
        {

        }

    }
}
