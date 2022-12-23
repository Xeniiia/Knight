using Games.KnightsMove.Scripts.PlayingField.Board;

namespace Games.KnightsMove.Scripts.LevelsControl
{
    public interface ILevelControl
    {
        int CurrentNumOfMoves { get; set; }
        bool CheckMoveByNumMovement(ICellsControl[] selectedCells);
        bool EndOfMoves();
        void DecrementNumOfMoves();
        void DestroyCellItems();
    }
}