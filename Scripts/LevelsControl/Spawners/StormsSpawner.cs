using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.Spawners
{
    public class StormsSpawner : IStormsSpawner
    {
        private readonly StormsCreator _stormsCreator;

        public StormsSpawner(StormsCreator stormsCreator)
        {
            _stormsCreator = stormsCreator;
        }

        public ICellItem[] SpawnStorms(SpawnerDTO dto, IFigure figure, Vector2Int figurePos)
        {
            return _stormsCreator.CreateStorms(dto, figure, figurePos);
        }
    }
}