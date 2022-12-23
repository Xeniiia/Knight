using Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories;

namespace Games.KnightsMove.Scripts.PlayingField.Board
{
    public interface ICellFactory
    {
        ICellsControl GetCell(InstCellDTO dto);

        public void InversionColor();
    }
}
