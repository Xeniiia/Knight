using System;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.CellsItems.Storms;
using UnityEngine.Localization.Settings;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels
{
    public class LevelWithStorms : StormsLevelElement   //todo: rename to DefaultLevelWithStorms
    {
        protected override IStandardSpawner GetSpawner()
        {
            var figureCreator = new FigureCreator();
            var goalCreator = new GoalCreator();
            return new StandardSpawner(figureCreator, goalCreator);
        }

        protected override string GetStringTask()
        {
            var localizedTask =
                LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "TaskWithStorms");
            
            return localizedTask;
        }


        protected override IStormsSpawner GetStormsSpawner()
        {
            var stormsCreator = new StormsCreator();
            return new StormsSpawner(stormsCreator);
        }
        
        
        public override void DestroyCellItems()
        {
            foreach (var storm in Storms)
            {
                (storm as Storm)?.DestroySelf();
            }

            Storms = Array.Empty<ICellItem>();
        }
    }
}


