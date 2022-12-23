using System;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface IActionHolder
    {
        Action GetWinAction();
        Action GetPitAction();
        Action GetStormAction();
    }
}