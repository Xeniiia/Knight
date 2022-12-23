using System;
using System.Collections.Generic;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.CellsItems.Pits;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels
{
    public class LevelWithPits : PitsLevelElement
    {
        protected override IPitsSpawner GetPitsSpawner()
        {
            var pitsCreator = new PitsCreator();
            return new PitsSpawner(pitsCreator);
        }


        protected override IStandardSpawner GetSpawner()
        {
            var figureCreator = new FigureCreator();
            var goalCreator = new GoalCreator();
            return new StandardSpawner(figureCreator, goalCreator);
        }

        protected override string GetStringTask()
        {
            var localizedTask =
                LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "TaskWithPits");
            
            return localizedTask;
        }


        public override void DestroyCellItems()
        {
            foreach (var pit in Pits)
            {
                (pit as Pit)?.DestroySelf();
            }

            Pits = Array.Empty<ICellItem>();
        }
    }
}