using GoBangClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBangClient.Util
{
    /// <summary>
    /// 棋子坐标
    /// </summary>
    public class Piece
    {
        int x;
        int y;
        int flag;

        public Piece(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.flag = GameController.ChessPieces[x, y];
        }

        public Piece(int x, int y, int flag)
        {
            this.x = x;
            this.y = y;
            this.flag = flag;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Flag { get => flag; set => flag = value; }
    }
}
