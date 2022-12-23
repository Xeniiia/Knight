using System;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Holders
{
    public interface IGoalFactory
    {
        ICellItem GetGoal(Vector2 pos, float sizeCell, Action winAction);
    }
}
