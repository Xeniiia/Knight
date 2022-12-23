using System;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface IPositionsObserver
    {
        event Action<Vector2[]> PositionSelected;
        void ResetMiddleDepthForCells();
    }
}
