using System;
using System.Collections;
using Games.KnightsMove.Scripts.PlayingField;
using Main.Menu.NewMenu.Scripts.Screens;
using Main.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Games.KnightsMove.Scripts.UI
{
    public class RestartGameUI : MonoBehaviour, IRestartGameUI
    {
        [SerializeField] private RewardViewPopup endOfGamePopup;
        [SerializeField] private EndOfMovesPopup endOfMovesPopup;
        [SerializeField] private UIView blocker;
        private Coroutine _endGamePopupShow;
        private Coroutine _endMovesPopupShow;
        
        public Action RestartLevel { get; set; }
        public Action RestartGame { get; set; }
        
        
        
        public void ShowEndOfMovesPopup()
        {
            _endMovesPopupShow = StartCoroutine(ShowEndMovesPopup());
        }
        
        private IEnumerator ShowEndMovesPopup()
        {
            endOfMovesPopup.OnPositiveEvent += RestartLevelAction;
            endOfGamePopup.OnNegativeEvent += ExitFromGame;

            blocker.IsVisible = true;
            endOfMovesPopup.IsVisible = true;
            yield return null;
        }
        
        private void RestartLevelAction()
        {
            endOfMovesPopup.OnPositiveEvent -= RestartLevelAction;
            endOfGamePopup.OnNegativeEvent -= ExitFromGame;
            StopCoroutine(_endMovesPopupShow);
            endOfMovesPopup.IsVisible = false;
            blocker.IsVisible = false;
            RestartLevel.Invoke();
        }
        
        
        public void ShowEndOfGamePopup(Sprite rewardSprite, string rewardName)
        {
            _endGamePopupShow = StartCoroutine(ShowFinalPopup(rewardSprite, rewardName));
        }

        private IEnumerator ShowFinalPopup(Sprite rewardSprite, string rewardName)
        {
            endOfGamePopup.OnPositiveEvent += RestartGameAction;
            endOfGamePopup.OnNegativeEvent += ExitFromGame;

            blocker.IsVisible = true;
            endOfGamePopup.SetRewardInfo(rewardSprite, rewardName);
            endOfGamePopup.VisibleWithCountdown(false);
            yield return new WaitWhile(() => endOfGamePopup.IsVisible);
            yield return null;
            blocker.IsVisible = false;

            endOfGamePopup.OnPositiveEvent -= RestartGameAction;
            endOfGamePopup.OnNegativeEvent -= ExitFromGame;
        }

        private void RestartGameAction()
        {
            endOfGamePopup.OnPositiveEvent -= RestartGameAction;
            endOfGamePopup.OnNegativeEvent -= ExitFromGame;
            StopCoroutine(_endGamePopupShow);
            endOfGamePopup.IsVisible = false;
            blocker.IsVisible = false;
            RestartGame.Invoke();
        }

        private void ExitFromGame()
        {
            GameLoader.Return();
        }
    }
}