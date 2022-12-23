namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories.ColorsOfCell
{
    internal class WhiteCell : IInstantiateCell
    {
        public IInstantiateCell InversionColor()
        {
            return new BlackCell();
        }

        public void ChangeColor(Cell cell)
        {
            cell.SetSprite(false);
        }
    }
}
