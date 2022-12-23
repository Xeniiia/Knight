using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl
{
    public interface IStandardSpawner
    {
        Vector2Int SpawnFigure(SpawnerDTO dto, out IFigure figure);
        ICellsControl SpawnGoal(SpawnerDTO dto, IFigure figure, Vector2Int figurePos, int stepsToGoal);
    }
}