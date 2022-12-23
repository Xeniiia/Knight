using System;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Holders
{
    public interface IStormFactory
    {
        ICellItem GetStorm(Vector2 pos, float sizeCell, Action stepAction);
    }
}