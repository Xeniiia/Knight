using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board
{
    public interface ICellsControl
    {
        Vector2 Position { get; }
        void ActionOnCorrectSelect();
        void ActionOnErrorSelect();
        bool CallResultState();
        void ResetOffset();
        ICellItem CellItem { get; set; }
        void DeleteCellItem();
        Vector2Int GetLocalPos();
    }
}
