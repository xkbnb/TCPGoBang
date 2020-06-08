using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGoBang.Model
{
    public class GameRoom
    {
        private string roomName;
        private GamePlayer gamePlayerMaseter;
        private GamePlayer gamePlayerParticipant;
        private int playerNumber;
        private int readyNumber;

        public GameRoom(string roomName, GamePlayer gamePlayerMaseter)
        {
            this.roomName = roomName;
            this.gamePlayerMaseter = gamePlayerMaseter;
            this.playerNumber = 1;
            this.readyNumber = 0;
        }

        public string RoomName { get => roomName; set => roomName = value; }
        public GamePlayer GamePlayerMaseter { get => gamePlayerMaseter; set => gamePlayerMaseter = value; }
        public GamePlayer GamePlayerParticipant { get => gamePlayerParticipant; set => gamePlayerParticipant = value; }
        public int PlayerNumber { get => playerNumber; set => playerNumber = value; }
        public int ReadyNumber { get => readyNumber; set => readyNumber = value; }

        /// <summary>
        /// 玩家退出房间
        /// </summary>
        /// <param name="gamePlayer"></param>
        /// <returns></returns>
        public bool QuitRoom(GamePlayer gamePlayer)
        {
            playerNumber--;
            if (gamePlayerMaseter.Equals(gamePlayer))        //房主退出
            {
                gamePlayerMaseter = playerNumber == 0 ? null : gamePlayerParticipant;      //房间中玩家不为0时非房主成为玩家
                gamePlayerParticipant = null;
                return false;
            }
            else           //非房主退出
            {
                gamePlayerParticipant = null;
                return true;
            }

        }

        /// <summary>
        /// 玩家加入房间
        /// </summary>
        /// <param name="gamePlayer"></param>
        public void JoinRoom(GamePlayer gamePlayer)
        {
            GamePlayerParticipant = gamePlayer;
            PlayerNumber++;
        }

        public override bool Equals(object obj)
        {
            if (obj is GameRoom)
            {
                GameRoom r = (GameRoom)obj;
                if (r.roomName == roomName)
                    return true;
            }
            return false;
        }
    }
}
