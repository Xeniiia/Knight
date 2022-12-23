using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.Spawners
{
    public class PitsAndStormsSpawner : IPitsAndStormsSpawner
    {
        private readonly PitsCreator _pitsCreator;
        private readonly StormsCreator _stormsCreator;

        
        public PitsAndStormsSpawner(PitsCreator pitsCreator, StormsCreator stormsCreator)
        {
            _pitsCreator = pitsCreator;
            _stormsCreator = stormsCreator;
        }

        
        public ICellItem[] SpawnPits(SpawnerDTO dto, IFigure figure, Vector2Int figurePos)
        {
            return _pitsCreator.CreatePits(dto, figure, figurePos);
        }

        
        public ICellItem[] SpawnStorms(SpawnerDTO dto, IFigure figure, Vector2Int figurePos)
        {
            return _stormsCreator.CreateStorms(dto, figure, figurePos);
        }
    }
}