using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories
{
    public class CellFactory : ICellFactory
    {
        [SerializeField] private Cell cellProto;
        private IInstantiateCell _instantiateCell;

        public CellFactory(IInstantiateCell instantiateCell, Cell cellProto)
        {
            _instantiateCell = instantiateCell;
            this.cellProto = cellProto;
        }


        public ICellsControl GetCell(InstCellDTO dto)
        {
            var cell = Object.Instantiate(cellProto, dto.NextPos, Quaternion.identity, dto.ParentTransform);
            cell.Init(dto.CellSelection, dto.LocalPos);
            cell.transform.localScale = new Vector2(dto.Size, dto.Size);
            _instantiateCell.ChangeColor(cell);
            return cell;
        }


        public void InversionColor()
        {
            _instantiateCell = _instantiateCell.InversionColor();
        }
    }
}
