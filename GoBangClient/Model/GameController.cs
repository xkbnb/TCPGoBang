using GoBangClient.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPGoBang;

namespace GoBangClient.Model
{
    class GameController
    {
        private static int[,] chessPieces = new int[15, 15];
        private static List<Piece> pieceList = new List<Piece>();
        private TcpClienter tcpClienter;
        private int depth;

        public static int[,] ChessPieces { get => chessPieces; set => chessPieces = value; }
        public static List<Piece> PieceList { get => pieceList; set => pieceList = value; }
        public TcpClienter TcpClienter { get => tcpClienter; set => tcpClienter = value; }

        public GameController(int depth)
        {
            TcpClienter = new TcpClienter();
            TcpClienter.DisConnectedServer += new TcpClienter.NetEvent(Program.ServerClose);
            this.depth = depth;
        }

        public void Close()
        {
            if (tcpClienter.IsConnected)
            {
                tcpClienter.Close();
            }
        }

        /// <summary>
        /// 添加棋子
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool AddPiece(Piece piece)
        {
            ChessPieces[piece.X, piece.Y] = piece.Flag;
            if (isVictory(piece.X, piece.Y))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 判断是否胜利
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool isVictory(int x, int y)
        {
            int max = 0;
            int tempXIndex = x;
            int tempYIndex = y;
            // 三维数组记录横向，纵向，左斜，右斜的移动
            int[,,] dir = new int[,,]
            {
                // 横向
				{ { -1, 0 }, { 1, 0 } },
				// 竖着
				{ { 0, -1 }, { 0, 1 } },
				// 左斜
				{ { -1, -1 }, { 1, 1 } },
				// 右斜
				{ { 1, -1 }, { -1, 1 } }
            };
            for (int i = 0; i < 4; i++)
            {
                int count = 1;
                for (int j = 0; j < 2; j++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        tempXIndex = tempXIndex + dir[i, j, 0];
                        tempYIndex = tempYIndex + dir[i, j, 1];
                        if (tempXIndex >= 0 && tempXIndex < 15 && tempYIndex >= 0 && tempYIndex < 15)
                        {
                            if (ChessPieces[tempXIndex, tempYIndex] == ChessPieces[x, y])
                            {
                                count++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    tempXIndex = x;
                    tempYIndex = y;
                }
                if (count >= 5)
                {
                    max = 1;
                    break;
                }
            }
            if (max == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Refresh()
        {
            chessPieces = new int[15, 15];
            pieceList = new List<Piece>();
        }
    }
}
