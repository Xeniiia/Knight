using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells
{
    public interface ICellSelection
    {
        void CellSelected(Vector2 localPos);

        void CellUnselected(Vector2 localPos);
    }
}
