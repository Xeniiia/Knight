namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories
{
    public interface IInstantiateCell
    {
        void ChangeColor(Cell cell);

        IInstantiateCell InversionColor();
    }
}
