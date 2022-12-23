using Games.KnightsMove.Scripts.PlayingField;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.Spawners
{
    public struct LevelInfoDTO
    {
        public ICellsControl CellWithGoalNow { get; set; }
        public IFigure Figure { get; set; }
        public Vector2Int FigurePos { get; set; }
        public ILevelControl Level { get; set; }
    }
}