using System;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures.FiguresTypes
{
    public class RookFigure : Figure //Ладья
    {
        public override ICellBehaviour CheckingPossibilityOfMakingMove(Vector2[] localPos, Vector2Int figurePos, out Vector2[] foundPathArray)
        {
            throw new NotImplementedException();
        }

        public override Vector2Int GetPositionByMoves(Vector2 boardSize, int depth, int localPosX, int localPosY)
        {
            throw new NotImplementedException();
        }
    }
}
