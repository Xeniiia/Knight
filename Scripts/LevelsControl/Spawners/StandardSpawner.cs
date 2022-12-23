using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.Spawners
{
    public class StandardSpawner : IStandardSpawner
    {
        private readonly FigureCreator _figureCreator;
        private readonly GoalCreator _goalCreator;

        public StandardSpawner(FigureCreator figureCreator, GoalCreator goalCreator)
        {
            _figureCreator = figureCreator;
            _goalCreator = goalCreator;
        }

        
        public Vector2Int SpawnFigure(SpawnerDTO dto, out IFigure figure)
        {
            return _figureCreator.CreateFigure(dto, out figure);
        }

        
        public ICellsControl SpawnGoal(SpawnerDTO dto, IFigure figure, Vector2Int figurePos, int stepsToGoal)
        {
            return _goalCreator.CreateGoal(dto, figure, figurePos, stepsToGoal);
        }
    }
}