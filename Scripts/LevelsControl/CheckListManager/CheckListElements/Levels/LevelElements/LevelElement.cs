using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelItemsCreators;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board;
using Games.Shapes.Scripts;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Levels.LevelElements
{
    public abstract class LevelElement : CheckListElement, ILevelControl
    {
        [SerializeField] protected int minStepsToGoal;
        [SerializeField] protected int maxStepsToGoal;
        protected int StepsToGoal;
        protected TaskController TaskController;
        public int CurrentNumOfMoves { get; set; } = -1;
        

        private void Awake()
        {
            TaskController = TaskController.NewInstance(this, "Games_KnightsMove");
            RandomizeStepsToGoal();
        }

        private void RandomizeStepsToGoal()
        {
            StepsToGoal = Random.Range(minStepsToGoal, maxStepsToGoal + 1);
        }
        
        
        public override LevelInfoDTO Execute(SpawnerDTO spawnDto)
        {
            var spawner = GetSpawner();
            var stepsToGoal = GetStepsToGoal();
            var figurePos = spawner.SpawnFigure(spawnDto, out var figure);
            var cellWithGoal = spawner.SpawnGoal(spawnDto, figure, figurePos, stepsToGoal);
            InitTask(GetStringTask());
            
            return PackageDto(cellWithGoal, figure, figurePos);
        }

        protected abstract IStandardSpawner GetSpawner();

        private int GetStepsToGoal() => StepsToGoal;
        
        private LevelInfoDTO PackageDto(ICellsControl cellWithGoalNow, IFigure figure, Vector2Int figurePos)
        {
            return new LevelInfoDTO
            {
                Level = this,
                CellWithGoalNow = cellWithGoalNow,
                Figure = figure,
                FigurePos = figurePos
            };
        }

        protected abstract string GetStringTask();

        protected void InitTask(string key)
        {
            TaskController.NewTask();
            TaskController.AddStringTask(key, false);
            TaskController.SetTask();
        }


        public virtual bool CheckMoveByNumMovement(ICellsControl[] selectedCells) => true;


        public virtual bool EndOfMoves() => false;
        
        
        public virtual void DecrementNumOfMoves() { }

        
        public virtual void DestroyCellItems() { }
    }
}