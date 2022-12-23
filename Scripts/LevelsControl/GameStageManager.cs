using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl
{
    public abstract class GameStageManager : MonoBehaviour
    {
        public abstract LevelInfoDTO Perform(SpawnerDTO spawnDto);
        public abstract void Next();
        public abstract void Restart(IFigure figure);
        
        public abstract event System.Action CurrentElementChanged;
        public abstract event System.Action<Sprite, string> RewardAchieved;
        public abstract event System.Action<Sprite, string> CheckListItemRanOut;
    }
}