using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements
{
    public interface IPitsSpawner
    {
        ICellItem[] SpawnPits(SpawnerDTO dto, IFigure figure, Vector2Int figurePos);
    }
}