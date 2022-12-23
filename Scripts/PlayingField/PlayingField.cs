using System;
using System.Collections;
using System.Linq;
using Games.KnightsMove.Scripts.CoroutineExtension;
using Games.KnightsMove.Scripts.LevelsControl;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField.Board;
using Games.KnightsMove.Scripts.PlayingField.Holders;
using Games.KnightsMove.Scripts.PlayingField.Holders.ForActions;
using Games.KnightsMove.Scripts.PlayingField.Holders.ForFactories;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public class PlayingField : MonoBehaviour, IPlayingField
    {
        [SerializeField] private AudioSource win;
        [SerializeField] private AudioSource lose;
        [SerializeField] private GameStageManager checkList;
        private ICellCollection _board;
        private IRestartGameUI _restartGameUI;
        private IPositionsObserver _observer;
        private ICellsControl[] _selectedCells;
        private ICellsControl _cellWithGoalNow;
        private IFactoryHolder _factoryHolder;
        private IActionHolder _actionHolder;
        private IFigure _figure;
        private Vector2Int _figurePos;
        private ILevelControl _level;
        private int _currentNumOfMoves;

        public event Action<int> UpdateNumOfMoves;
        public event Action<Sprite, string> RewardWasAchieved;

        private int CurrentNumOfMoves
        {
            get => _currentNumOfMoves;

            set
            {
                _currentNumOfMoves = value;
                UpdateNumOfMoves?.Invoke(value);
            }
        }


        private void Awake()
        {
            _observer = GetComponent<IPositionsObserver>();
            _board = GetComponent<ICellCollection>();
            _restartGameUI = GetComponent<IRestartGameUI>();
            var figureFactory = GetComponent<IFigureFactory>();
            var goalFactory = GetComponent<IGoalFactory>();
            var pitFactory = GetComponent<IPitFactory>();
            var stormFactory = GetComponent<IStormFactory>();
            _factoryHolder = new FactoryHolder(figureFactory, goalFactory, pitFactory, stormFactory);
            _actionHolder = new ActionHolder(GenerationNewLevel, PitAction, StormAction);

        }

        private void PitAction()
        {
            _figurePos = _figure.ReturnOnStartPos();    //todo: trigger
        }

        private void StormAction()
        {
            _figurePos = _figure.RebaseOnRandomPos(_board); //todo: trigger
        }
        
        private void OnEnable()
        {
            checkList.CheckListItemRanOut += FinishGame;
            checkList.CurrentElementChanged += SpawnPlayingObjects;
            checkList.RewardAchieved += ActivateRewardView;
        }

        private void ActivateRewardView(Sprite rewardSprite, string rewardName)
        {
            RewardWasAchieved?.Invoke(rewardSprite, rewardName);
            _observer.PositionSelected -= SelectPositionHandler;
        }


        public void ResumeGame()
        {
            _observer.PositionSelected += SelectPositionHandler;
            SpawnPlayingObjects();
        }
        

        private void FinishGame(Sprite rewardSprite, string rewardName)
        {
            _restartGameUI.ShowEndOfGamePopup(rewardSprite, rewardName);
        }


        private void Start()
        {
            _observer.PositionSelected += SelectPositionHandler;
            //CalibrationDataHolder.CalibrationDataIsReady += StartGeneration;
            //if (CalibrationDataHolder.CalibrationExists) StartGeneration();
            _restartGameUI.RestartLevel += RestartLevel;
            _restartGameUI.RestartGame += RestartGame;
        }

        private void RestartGame()
        {
            _figure?.StopMove();
            _level?.DestroyCellItems();
            _board.ClearBoard();
            checkList.Restart(_figure);
            SpawnPlayingObjects();
        }


        public void RestartLevel()
        {
            _figure?.StopMove();
            _level?.DestroyCellItems();
            _board.ClearBoard();
            SpawnPlayingObjects();
        }

        
        [Button]
        private void SpawnPlayingObjects()
        {
            var spawnDto = new SpawnerDTO
            {
                Board = _board,
                FactoryHolder = _factoryHolder,
                ActionHolder = _actionHolder
            };
            
            var levelInfoDto = checkList.Perform(spawnDto);
            HandleLevelDto(levelInfoDto);
            CurrentNumOfMoves = _level.CurrentNumOfMoves; //todo: в другом месте устанавливать
        }

        private void GenerationNewLevel()
        {
            win.Play();
            _level?.DestroyCellItems();
            _board.ClearBoard();
            checkList.Next();
        }

        private void HandleLevelDto(LevelInfoDTO dto)
        {
            if (dto.Level == null)
            {
                _level?.DestroyCellItems();
                _board.ClearBoard();
                checkList.Next();
                return;
            }
            _level = dto.Level;
            _figure = dto.Figure;
            _figurePos = dto.FigurePos;
            _cellWithGoalNow = dto.CellWithGoalNow;
        }


        private void SelectPositionHandler(Vector2[] localPos)
        {
             var cellBehaviour = _figure.CheckingPossibilityOfMakingMove(localPos, _figurePos, out var foundPath);
            _selectedCells = _board.GetCellsOnLocalCoordinates(foundPath);

            if (TryMoveFigure(_selectedCells, cellBehaviour))
            {
                UpdateLocalPosFigure(foundPath);
            }
            
            
            // todo
            // var isPathExists = _figure.CheckingPo -> true - если найден, false - если не найден. out foundPath
            // if(!isPathExist) return;
            // var selectedPath = _board.GetCellOnLocalCoordinates(foundPath);
            // if(!_level.CheckMoveByNumMovement( s  )) return;
            // _figure.GO(selectedPath, completedAction);
        }

        private void UpdateLocalPosFigure(Vector2[] foundPath)
        {
            var lastCell = foundPath.Last();
            _figurePos = new Vector2Int((int)lastCell.x, (int)lastCell.y);
        }

        private bool TryMoveFigure(ICellsControl[] selectedCells, ICellBehaviour cellBehaviour)
        {
            var move = false;
            if (_level.CheckMoveByNumMovement(selectedCells) &&
                (move = cellBehaviour.EffectSelectedCell(_board, selectedCells, out var lastCoroutine, out var firstCoroutine)))
            {
                _level.DecrementNumOfMoves();
                _observer.PositionSelected -= SelectPositionHandler;
                lastCoroutine.AddCallback(Vector2.zero, Callback);
                firstCoroutine.StartCoroutine();
            }

            return move;
        }

        private IEnumerator Callback(Vector2 vector, CoroutineSequence coroutine)
        {
            var goalArrival = _selectedCells.Last().CallResultState();
            if (!goalArrival && _level.EndOfMoves()) //todo: сделать подписку на победу
            {
                lose.Play();
                _restartGameUI.ShowEndOfMovesPopup();
            }
            _board.ResetMiddleDepthForCells();
            _board.ResetOffsetDepthForCells();

            CurrentNumOfMoves = _level.CurrentNumOfMoves;
            _observer.PositionSelected += SelectPositionHandler;
            yield break;
        }
    }
}