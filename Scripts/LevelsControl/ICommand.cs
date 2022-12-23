using Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl
{
    public interface ICommand
    {
        LevelInfoDTO Execute(SpawnerDTO spawnDto);
        void ExecuteUndo(IFigure figure);
        CheckPoint GetCheckPoint();
        Sprite GetLevelSprite();
    }
}