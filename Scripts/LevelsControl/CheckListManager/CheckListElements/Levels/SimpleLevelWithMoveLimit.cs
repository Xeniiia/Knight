using System.Collections.Generic;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels
{
    public class SimpleLevelWithMoveLimit : LevelElement
    {
        [Tooltip("Use if available number of moves may be more than steps to goal.")]
        [SerializeField] private bool moreAvailableMoves;

        [SerializeField] private int offsetMovesFrom;
        [SerializeField] private int offsetMovesTo = 4;
        private int _availableNumOfMoves;

        
        private void RandomizeNumOfMoves()
        {
            _availableNumOfMoves = moreAvailableMoves
                ? Random.Range(StepsToGoal + offsetMovesFrom, StepsToGoal + offsetMovesTo)
                : StepsToGoal;
        }

        private void ResetNumOfMoves()
        {
            CurrentNumOfMoves = _availableNumOfMoves;
        }
        

        protected override IStandardSpawner GetSpawner()
        {
            var figureCreator = new FigureCreator();
            var goalCreator = new GoalCreator();
            return new StandardSpawner(figureCreator, goalCreator);
        }


        public override LevelInfoDTO Execute(SpawnerDTO spawnDto)
        {
            RandomizeNumOfMoves();
            ResetNumOfMoves();
            var dto = base.Execute(spawnDto);

            return dto;
        }

        protected override string GetStringTask()
        {
            var localizedTask =
                LocalizationSettings.StringDatabase.GetLocalizedString(
                    "Games_KnightsMove", 
                    "TaskWithMoveLimits", 
                    new List<object>{ _availableNumOfMoves });
            
            return localizedTask;
        }

        public override bool CheckMoveByNumMovement(ICellsControl[] selectedCells) => CurrentNumOfMoves > 0;

        
        public override bool EndOfMoves() => CurrentNumOfMoves == 0;

        public override void DecrementNumOfMoves() => CurrentNumOfMoves--;
    }
}