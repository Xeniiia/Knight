using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements
{
    public abstract class PitsLevelElement : LevelElement
    {
        protected ICellItem[] Pits;
        
        
        protected abstract IPitsSpawner GetPitsSpawner();
        
        public override LevelInfoDTO Execute(SpawnerDTO spawnDto)
        {
            var levelInfoDto = base.Execute(spawnDto);
            var pitsSpawner = GetPitsSpawner();
            Pits = pitsSpawner.SpawnPits(spawnDto, levelInfoDto.Figure, levelInfoDto.FigurePos);

            return levelInfoDto;
        }
    }
}