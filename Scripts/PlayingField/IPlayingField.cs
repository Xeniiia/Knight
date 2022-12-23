using System;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface IPlayingField
    {
        event Action<int> UpdateNumOfMoves;
        public event Action<Sprite, string> RewardWasAchieved;

        void RestartLevel();
        void ResumeGame();
    }
}
