using System;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.CellsItems.Pits;
using Games.KnightsMove.Scripts.PlayingField.CellsItems.Storms;
using UnityEngine.Localization.Settings;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels
{
    public class LevelWithPitsAndStorms : PitsAndStormsLevelElement 
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
                LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "TaskWithPitsAndStorms");
            
            return localizedTask;
        }


        protected override IPitsAndStormsSpawner GetPitsAndStormSpawner()
        {
            var pitsCreator = new PitsCreator();
            var stormsCreator = new StormsCreator();
            return new PitsAndStormsSpawner(pitsCreator, stormsCreator);
        }


        public override void DestroyCellItems()
        {
            foreach (var pit in Pits)
            {
                (pit as Pit)?.DestroySelf();
            }

            foreach (var storm in Storms)
            {
                (storm as Storm)?.DestroySelf();
            }

            Pits = Array.Empty<ICellItem>();
            Storms = Array.Empty<ICellItem>();
        }
    }
}