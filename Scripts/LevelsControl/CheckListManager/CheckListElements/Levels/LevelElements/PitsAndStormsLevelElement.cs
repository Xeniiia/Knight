using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements
{
    public abstract class PitsAndStormsLevelElement : LevelElement
    {
        protected ICellItem[] Pits;
        protected ICellItem[] Storms;
        
        protected abstract IPitsAndStormsSpawner GetPitsAndStormSpawner();
        
        public override LevelInfoDTO Execute(SpawnerDTO spawnDto)
        {
            var levelInfoDto = base.Execute(spawnDto);
            var pitsAndStormSpawner = GetPitsAndStormSpawner();
            Pits = pitsAndStormSpawner.SpawnPits(spawnDto, levelInfoDto.Figure, levelInfoDto.FigurePos);
            Storms = pitsAndStormSpawner.SpawnStorms(spawnDto, levelInfoDto.Figure, levelInfoDto.FigurePos);

            return levelInfoDto;
        }
    }
}