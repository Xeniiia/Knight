using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface ICellCollection
    {
        Vector2[] ParseToLocalCoordinates(Vector2[] posCell);

        ICellsControl[] GetCellsOnLocalCoordinates(Vector2[] pos);

        ICellsControl GetRandomCell(out Vector2Int pos);

        void ClearBoard();

        Vector2 CellCount { get; }
        float SizeOfSideCell { get; }

        ICellsControl this[int x, int y]
        {
            get;
        }

        void ResetMiddleDepthForCells();
        void ResetOffsetDepthForCells();
    }
}
