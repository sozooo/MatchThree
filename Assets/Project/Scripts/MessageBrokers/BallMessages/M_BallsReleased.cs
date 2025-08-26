using System.Collections.Generic;
using Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas;

namespace Project.Scripts.MessageBrokers.BallMessages
{
    public struct M_BallsReleased
    {
        public M_BallsReleased(List<BallData> ballDatas)
        {
            BallDatas = ballDatas;
        }

        public List<BallData> BallDatas { get; }
    }
}