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
    public class PitsCreator
    {
        public ICellItem[] CreatePits(SpawnerDTO dto, IFigure figure, Vector2Int figurePos)    //todo: Переделать
        {
            var pits = new List<ICellItem>();
            var board = dto.Board;
            var pitFactory = dto.FactoryHolder.GetPitFactory();
            var pitAction = dto.ActionHolder.GetPitAction();

            for (int i = 1; i < 3; i++)
            {
                pits.Add(CreatePit(figure, figurePos, i, board, pitFactory, pitAction));
            }

            pits.Add(CreatePitOnRandomCell(board, pitFactory, pitAction, figurePos));

            return pits.ToArray();
        }

        private static ICellItem CreatePit(IFigure figure, Vector2Int figurePos, int depth, ICellCollection board,
            IPitFactory pitFactory, Action pitAction)
        {
            var sizeCell = board.SizeOfSideCell;
            var cellWithPit = GetCell(figure, figurePos, depth, board);
            while (cellWithPit.CellItem != null)
            {
                cellWithPit = GetCell(figure, figurePos, depth, board);
            }
            var pit = pitFactory.GetPit(cellWithPit.Position, sizeCell, pitAction);
            cellWithPit.CellItem = pit;

            return pit;
        }

        private static ICellsControl GetCell(IFigure figure, Vector2Int figurePos, int depth, ICellCollection board)
        {
            var boardSize = board.CellCount;
            var newPitPos = figure.GetPositionByMoves(boardSize, depth, figurePos.x, figurePos.y);
            var cellWithPit = board[newPitPos.x, newPitPos.y];
            
            return cellWithPit;
        }

        private ICellItem CreatePitOnRandomCell(ICellCollection board, IPitFactory pitFactory, Action pitAction, Vector2Int figurePos)
        {
            var randomCell = board.GetRandomCell(out var localPos);
            while (randomCell.CellItem != null || localPos == figurePos)
            {
                randomCell = board.GetRandomCell(out localPos);
            }
            var sizeCell = board.SizeOfSideCell;
            var pit = pitFactory.GetPit(randomCell.Position, sizeCell, pitAction);
            randomCell.CellItem = pit;

            return pit;
        }
    }
}