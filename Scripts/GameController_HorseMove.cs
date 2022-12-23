using System.Collections;
using Backend.Components.MultiSlider.Scripts;
using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.UI;
using Games.Shapes.Scripts;
using Main.Menu.NewMenu.Scripts.Notifications.Common;
using Main.Menu.NewMenu.Scripts.Screens;
using Main.Scripts;
using Main.Scripts.ScensGeneral;
using UnityEngine;

namespace Games.KnightsMove.Scripts
{
    public class GameController_HorseMove : GameControllerBase
    {
        [SerializeField] private RewardViewPopup rewardPopup;
        [SerializeField] private NotificationView startGamePopup;
        [SerializeField] private UIView blocker;
        private IPlayingField _playingField;
        private IPositionsObserver _board;
        private Coroutine _rewardPopupShow;


        protected override void OnEnabled()
        {
            CreateMultiSlider();
        }

        private void CreateMultiSlider()
        {
            MultiSliderHandle[] sliderHandles = new[] {
                new MultiSliderHandle(MultiSliderHandleType.Etalon, Color.white, 0.5f)
            };
            var multiSlider = MultiSlider.Create(sliderHandles);
        }


        protected override void OnDelayedStart()
        {
            _playingField = GetComponent<IPlayingField>();
            _board = GetComponent<IPositionsObserver>();
            _playingField.RewardWasAchieved += ShowRewardPopup;

            StartCoroutine(ShowStartGamePopup());
        }

        private void ShowRewardPopup(Sprite rewardSprite, string rewardName)
        {
            _rewardPopupShow = StartCoroutine(ShowFinalPopup(rewardSprite, rewardName));
        }

        private IEnumerator ShowFinalPopup(Sprite rewardSprite, string rewardName)
        {
            rewardPopup.OnPositiveEvent += Resume;
            rewardPopup.OnNegativeEvent += ExitFromGame;

            blocker.IsVisible = true;
            rewardPopup.SetRewardInfo(rewardSprite, rewardName);
            rewardPopup.VisibleWithCountdown(true);
            yield return new WaitWhile(() => rewardPopup.IsVisible);
            yield return null;
            blocker.IsVisible = false;

            rewardPopup.OnPositiveEvent -= Resume;
            rewardPopup.OnNegativeEvent -= ExitFromGame;
        }

        
        private IEnumerator ShowStartGamePopup()
        {
            startGamePopup.OnPositiveEvent += StartGame;
            blocker.IsVisible = true;
            startGamePopup.IsVisible = true;
            yield return null;
        }

        private void StartGame()
        {
            startGamePopup.OnPositiveEvent -= StartGame;
            startGamePopup.IsVisible = false;
            blocker.IsVisible = false;
            
            CreateTask();
        }

        
        private void Resume()
        {
            rewardPopup.OnPositiveEvent -= Resume;
            rewardPopup.OnNegativeEvent -= ExitFromGame;
            StopCoroutine(_rewardPopupShow);
            rewardPopup.IsVisible = false;
            blocker.IsVisible = false;
            _playingField.ResumeGame();
        }

        private void ExitFromGame()
        {
            GameLoader.Return();
        }

        
        protected override void CreateTask()
        {
            _playingField.RestartLevel();
        }


        protected override void OnCalibrationIsDestroy()
        {
            _board.ResetMiddleDepthForCells();
            CreateMultiSlider();
        }
    }
}

