using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements
{
    public abstract class StormsLevelElement : LevelElement
    {
        protected ICellItem[] Storms;
        
        
        public override LevelInfoDTO Execute(SpawnerDTO spawnDto)
        {
            var levelInfoDto = base.Execute(spawnDto);
            var stormsSpawner = GetStormsSpawner();
            Storms = stormsSpawner.SpawnStorms(spawnDto, levelInfoDto.Figure, levelInfoDto.FigurePos);

            return levelInfoDto;
        }
        
        
        protected abstract IStormsSpawner GetStormsSpawner();
    }
}