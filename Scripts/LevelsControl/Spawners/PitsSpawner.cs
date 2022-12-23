using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.Spawners
{
    public class PitsSpawner : IPitsSpawner
    {
        private readonly PitsCreator _pitsCreator;

        public PitsSpawner(PitsCreator pitsCreator)
        {
            _pitsCreator = pitsCreator;
        }

        public ICellItem[] SpawnPits(SpawnerDTO dto, IFigure figure, Vector2Int figurePos)
        {
            return _pitsCreator.CreatePits(dto, figure, figurePos);
        }
    }
}