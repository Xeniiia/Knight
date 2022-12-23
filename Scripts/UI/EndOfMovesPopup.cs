using Games.TrafficCode.Scripts.UI;
using Main.Menu.NewMenu.Scripts.Notifications.Common;
using Main.Menu.Scripts.Utility;
using Main.Scripts;
using UnityEngine;

namespace Games.KnightsMove.Scripts.UI
{
    [DrawProperties("continueButton", "flowerMover")]
    public class EndOfMovesPopup : NotificationView
    {
        [SerializeField] private ContinueButton continueButton;
        [SerializeField] private MoverList_Main flowerMover;
        private bool counter;

        public void VisibleWithCountdown(bool withCountdown)
        {
            counter = withCountdown;
            continueButton.SetCountdown(withCountdown);
            IsVisible = true;
        }


        public override void BeforeShow()
        {
            base.BeforeShow();
            OnNegativeEvent += ExitFromGame;

            if (counter && continueButton && Application.isPlaying)
                continueButton.StartCountdown(PositiveNotification);
        }


        public override void AfterShow()
        {
            base.AfterShow();

            if (flowerMover && Application.isPlaying)
                flowerMover.StartMover(true);
        }

        
        public override void BeforeHide()
        {
            base.BeforeHide();

            if (counter && continueButton && Application.isPlaying)
                continueButton.StopCountdown();

            if (flowerMover && Application.isPlaying)
                flowerMover.StartMover(false);

            counter = false;
        }

        public override void AfterHide()
        {
            base.AfterHide();
            OnNegativeEvent -= ExitFromGame;
        }


        private void ExitFromGame()
        {
            GameLoader.Return();
        }
    }
}