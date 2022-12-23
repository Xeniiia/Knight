using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using UnityEngine.Localization.Settings;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels
{
    public class SimpleLevel : LevelElement
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
                LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "TaskSimple");
            
            return localizedTask;
        }
    }
}