using GoBangClient.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBangClient.Model
{
    /// <summary>
    /// 棋盘的参数及地址转换
    /// </summary>
    public class CheckBoard
    {
        public static int distance = 40;
        public static int leftBorder = 60;
        public static int topBorder = 80;
        public static int lineNumber = 15;  //15*15大小的棋盘
        public static int chessPiecesSize = 40;

        /// <summary>
        /// 坐标转换为Point类型
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static Point ConvertCoordinatesToPoint(Piece piece)
        {
            int x, y;
            x = piece.X * distance + leftBorder - chessPiecesSize / 2;
            y = piece.Y * distance + topBorder - chessPiecesSize / 2;
            return new Point(x, y);
        }

        /// <summary>
        /// point类型转换为坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static Piece ConvertPointToCoordinates(Point point, int flag)
        {
            int x, y;
            Piece piece;
            if (point.X < leftBorder || point.Y < topBorder || point.X > (lineNumber - 1) * distance + leftBorder || point.Y > (lineNumber - 1) * distance + topBorder)
            {
                piece = new Piece(-1, -1, flag);
            }
            else
            {
                float i = ((float)point.X - leftBorder) / distance;
                float j = ((float)point.Y - topBorder) / distance;
                x = Convert.ToInt32(i);
                y = Convert.ToInt32(j);
                if (GameController.ChessPieces[x, y] != 0)
                {
                    piece = new Piece(-1, -1, flag);
                }
                else
                {
                    piece = new Piece(x, y, flag);
                }
            }
            return piece;
        }
    }
}
