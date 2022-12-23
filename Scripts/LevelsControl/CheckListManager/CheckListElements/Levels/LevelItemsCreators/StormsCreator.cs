using System;
using System.Collections.Generic;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.Holders;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators
{
    public class StormsCreator
    {
        public ICellItem[] CreateStorms(SpawnerDTO dto, IFigure figure, Vector2Int figurePos)
        {
            var storms = new List<ICellItem>();
            var board = dto.Board;
            var stormFactory = dto.FactoryHolder.GetStormFactory();
            var stormAction = dto.ActionHolder.GetStormAction();

            for (int i = 2; i < 5; i++)
            {
                var cellWithStorm = GetRandomCellByDepth(figure, figurePos, i, board);
                if (cellWithStorm.CellItem != null) continue;
                storms.Add(CreateStorm(cellWithStorm, board, stormFactory, stormAction));
            }
            
            for (int j = 0; j < 2; j++)
            {
                storms.Add(CreateStormOnRandomCell(board, stormFactory, stormAction, figurePos));
            }

            return storms.ToArray();
        }

        private static ICellItem CreateStorm(ICellsControl cellWithStorm, ICellCollection board, IStormFactory stormFactory, Action stormAction)
        {
            var sizeCell = board.SizeOfSideCell;
            var storm = stormFactory.GetStorm(cellWithStorm.Position, sizeCell, stormAction);
            cellWithStorm.CellItem = storm;

            return storm;
        }

        private static ICellsControl GetRandomCellByDepth(IFigure figure, Vector2Int figurePos, int depth, ICellCollection board)
        {
            var boardSize = board.CellCount;
            var newStormPos = figure.GetPositionByMoves(boardSize, depth, figurePos.x, figurePos.y);
            var cellWithStorm = board[newStormPos.x, newStormPos.y];
            return cellWithStorm;
        }

        private ICellItem CreateStormOnRandomCell(ICellCollection board, IStormFactory stormFactory, Action stormAction, Vector2Int figurePos)
        {
            var randomCell = board.GetRandomCell(out var localPos);
            while (randomCell.CellItem != null || localPos == figurePos)
            {
                randomCell = board.GetRandomCell(out localPos);
            }
            var sizeCell = board.SizeOfSideCell;
            var storm = stormFactory.GetStorm(randomCell.Position, sizeCell, stormAction);
            randomCell.CellItem = storm;

            return storm;
        }
    }
}