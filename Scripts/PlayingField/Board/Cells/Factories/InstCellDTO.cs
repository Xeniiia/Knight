using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories
{
    public struct InstCellDTO
    {
        public Vector2 NextPos { get; set; }
        public Transform ParentTransform { get; set; }
        public Vector2 LocalPos { get; set; }
        public ICellSelection CellSelection { get; set; }
        public float Size { get; set; }
    }
}
