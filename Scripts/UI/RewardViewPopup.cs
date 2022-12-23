using Games.TrafficCode.Scripts.UI;
using Main.Menu.NewMenu.Scripts.Notifications.Common;
using Main.Menu.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Games.KnightsMove.Scripts.UI
{
    [DrawProperties("continueButton", "figurePlate", "flowerMover", "rewardViewArea")]
    public class RewardViewPopup : NotificationView
    {
        [SerializeField] private ContinueButton continueButton;
        [SerializeField] private FinalPopupPlate figurePlate;
        [SerializeField] private MoverList_Main flowerMover;
        [SerializeField] private Image rewardViewArea;

        private bool counter;

        public void VisibleWithCountdown(bool withCountdown)
        {
            counter = withCountdown;
            continueButton.SetCountdown(withCountdown);
            IsVisible = true;
        }

        public void SetRewardInfo(Sprite newFigureSprite, string newSpriteName)
        {
            figurePlate.IsActive = true;
            SetMessage(newSpriteName);
            rewardViewArea.sprite = newFigureSprite;
        }


        public override void BeforeShow()
        {
            base.BeforeShow();

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
    }
}