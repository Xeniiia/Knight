using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements
{
    public abstract class CheckListElement : MonoBehaviour, ICommand
    {
        [SerializeField] protected CheckPoint checkPoint;
        [SerializeField] protected Sprite levelSprite;
        
        
        public abstract LevelInfoDTO Execute(SpawnerDTO spawnDto);

        
        public virtual void ExecuteUndo(IFigure figure) { }


        public CheckPoint GetCheckPoint() => checkPoint;
        
        
        public Sprite GetLevelSprite() => levelSprite;

        public void SetPointSprite() => checkPoint.SetLevelSprite(levelSprite);
    }
}