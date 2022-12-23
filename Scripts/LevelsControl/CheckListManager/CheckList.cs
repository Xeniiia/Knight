using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backend.Scripts.Main;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements;
using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Rewards;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using UnityEngine;
using UnityEngine.UI;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager
{
    public class CheckList : GameStageManager
    {
        [SerializeField] private Image filling;
        [SerializeField] private GameObject bottomPoint;
        [SerializeField] private GameObject topPoint;
        [SerializeField] private CheckPoint startPoint;
        [SerializeField] private Sprite startSprite;
        private List<CheckPoint> _currentCheckPoints;
        private List<CheckPoint> _allCheckPoints;
        private IEnumerator _fillingCoroutine;
        private ICommand[] _checkListItem;
        private int _currentRewardLevel = -1;
        private int _current;
        private int _currentPoint;

        private int CurrentPoint
        {
            get => _currentPoint;
            set
            {
                _currentPoint = value;
                CurrentElementChanged?.Invoke();
            }
        }

        public override event Action CurrentElementChanged;
        public override event Action<Sprite, string> RewardAchieved;
        public override event Action<Sprite, string> CheckListItemRanOut;


        private void Awake()
        {
            _checkListItem = GetComponentsInChildren<ICommand>();
            FillPoints();
            GenerateCheckPoints();
            CreateStartPoint();
            SetSpriteOnStartPoint(startSprite);
            filling.fillAmount = 0;
        }

        private void GenerateCheckPoints()
        {
            var points = GetBlockOfCheckPoints();
            CreatePoints(points);
        }

        private void FillPoints()
        {
            _allCheckPoints = new List<CheckPoint>();

            foreach (var element in _checkListItem)
            {
                var point = element.GetCheckPoint();
                point.SetLevelSprite(element.GetLevelSprite());
                _allCheckPoints.Add(point);
            }
        }

        private List<CheckPoint> GetBlockOfCheckPoints()
        {
            var points = new List<CheckPoint>();
            int i;
            for (i = 0; i < _allCheckPoints.Count; i++)
            {
                var point = _allCheckPoints[i];
                points.Add(point);
                if (point.GetCheckPointType() == PointType.Reward)
                {
                    i++;
                    break;
                }
            }

            _allCheckPoints.RemoveRange(0, i);
            _currentRewardLevel += i;

            return points;
        }

        private void CreatePoints(List<CheckPoint> points)
        {
            _currentCheckPoints = new List<CheckPoint>();
            if (points.Count > 2) points.RemoveAt(points.Count - 2);

            var posX = bottomPoint.transform.position.x;
            var minPosY = bottomPoint.transform.position.y;
            var maxPosY = topPoint.transform.position.y;
            var height = maxPosY - minPosY;
            var step = height / points.Count;
            var currentPosY = minPosY + step;
            CheckPoint newPoint = null;
            for (int i = 0; i < points.Count; i++)
            {
                newPoint = Instantiate(points[i], new Vector2(posX, currentPosY), Quaternion.identity);
                _currentCheckPoints.Add(newPoint);
                currentPosY += step;
            }

            if (newPoint != null) newPoint.SetLevelSprite(_checkListItem[_currentRewardLevel].GetLevelSprite());
        }

        private void CreateStartPoint()
        {
            var posX = bottomPoint.transform.position.x;
            var minPosY = bottomPoint.transform.position.y;
            startPoint = Instantiate(startPoint, new Vector2(posX, minPosY), Quaternion.identity);
        }


        private void SetSpriteOnStartPoint(Sprite sprite)
        {
            startPoint.SetLevelSprite(sprite);
        }


        public override LevelInfoDTO Perform(SpawnerDTO spawnDto)
        {
            return _checkListItem[_current].Execute(spawnDto);
        }


        public override void Next()
        {
            //todo: отписаться от старого события победы, подписаться на новое
            if (_current < _checkListItem.Length - 1)
            {
                _current++;
                if (CurrentPoint != _currentCheckPoints.Count)
                {
                    var newValue = filling.fillAmount + 1f / _currentCheckPoints.Count;
                    _fillingCoroutine = FillBar(newValue);
                    StartCoroutine(_fillingCoroutine);
                    _currentCheckPoints[CurrentPoint].MarkAsReceived();
                }
                else
                {
                    StartCoroutine(ChangePointsBlock());
                    var reward = _checkListItem[_current - 1] as Reward;
                    if (reward != null)
                    {
                        var rewardSprite = reward.GetRewardSprite();
                        var rewardName = reward.GetRewardName();
                        RewardAchieved?.Invoke(rewardSprite, rewardName);
                    }
                }
            }
            else
            {
                var reward = _checkListItem[_current] as Reward;
                if (reward != null)
                {
                    var rewardSprite = reward.GetRewardSprite();
                    var rewardName = reward.GetRewardName();
                    CheckListItemRanOut?.Invoke(rewardSprite, rewardName);
                }
            }
        }

        private IEnumerator UpdateFillSprite(float to)
        {
            to = to > 1 ? 1 : to;
            float time = 0;
            while (time <= 1.5f)
            {
                time += Time.deltaTime;
                filling.fillAmount = Mathf.Lerp(filling.fillAmount, to, time / 1.5f);
                yield return null;
            }
        }

        private IEnumerator FillBar(float to)
        {
            yield return UpdateFillSprite(to);
            CurrentPoint++;
        }

        private IEnumerator ChangePointsBlock()
        {
            yield return UpdateFillSprite(0);
            _currentPoint = 0;
            LoadNewCheckPoints();
        }

        private void LoadNewCheckPoints()
        {
            var last = _currentCheckPoints.Count - 1;
            SetSpriteOnStartPoint(_currentCheckPoints[last].GetLevelSprite());
            
            foreach (var oldPoint in _currentCheckPoints)
            {
                oldPoint.Destroy();
            }

            GenerateCheckPoints();
        }


        [Sirenix.OdinInspector.Button]
        public override void Restart(IFigure figure)
        {
            for (int i = _checkListItem.Length - 1; i >= 0; i--)
            {
                _checkListItem[i].ExecuteUndo(figure);
            }

            _current = 0;
            _currentRewardLevel = -1;
            _currentPoint = 0;
            StartCoroutine(UpdateFillSprite(0));
            FillPoints();
            LoadNewCheckPoints();
            SetSpriteOnStartPoint(startSprite);
        }
    }
}