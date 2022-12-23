using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators
{
    public class GoalCreator
    {
        public ICellsControl CreateGoal(SpawnerDTO dto, IFigure figure, Vector2Int figurePos, int stepsToGoal)
        {
            var newGoalPos = figure.GetPositionByMoves(dto.Board.CellCount, stepsToGoal, figurePos.x, figurePos.y);
            var cellForGoal = dto.Board[newGoalPos.x, newGoalPos.y];
            var sizeCell = dto.Board.SizeOfSideCell;
            var goalFactory = dto.FactoryHolder.GetGoalFactory();
            var winAction = dto.ActionHolder.GetWinAction();
            var goal = goalFactory.GetGoal(cellForGoal.Position, sizeCell, winAction);
            cellForGoal.CellItem = goal;

            return cellForGoal;
        }
    }
}