namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories.ColorsOfCell
{
    internal class BlackCell : IInstantiateCell
    {
        public IInstantiateCell InversionColor()
        {
            return new WhiteCell();
        }

        public void ChangeColor(Cell cell)
        {
            cell.SetSprite(true);
        }
    }
}
