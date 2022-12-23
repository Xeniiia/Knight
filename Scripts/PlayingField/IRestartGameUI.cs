using System;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface IRestartGameUI
    {
        Action RestartLevel { get; set; }
        Action RestartGame { get; set; }
        void ShowEndOfMovesPopup();
        void ShowEndOfGamePopup(Sprite rewardSprite, string rewardName);
    }
}
