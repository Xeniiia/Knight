using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators
{
    public class FigureCreator
    {
        public Vector2Int CreateFigure(SpawnerDTO dto, out IFigure figure)
        {
            var sizeCell = dto.Board.SizeOfSideCell;
            var randomCell = dto.Board.GetRandomCell(out var localPos);
            var figureFactory = dto.FactoryHolder.GetFigureFactory();
            figure = figureFactory.PlaceFigure(randomCell.Position, sizeCell);
            figure.InitStartPos(localPos);

            return localPos;
        }
    }
}