using System;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.Holders;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.CellsItems.Goals.Factories
{
    public class GoalFactory : MonoBehaviour, IGoalFactory
    {
        [SerializeField] private Goal proto;
        private Goal _goal;


        public ICellItem GetGoal(Vector2 pos, float sizeCell, Action winAction)
        {
            if (_goal != null)
            {
                _goal.transform.position = pos;
            }
            else
            {
                _goal = Instantiate(proto, pos, Quaternion.identity, transform);
                _goal.WinAction += winAction;
            }
            _goal.transform.localScale = new Vector2(sizeCell, sizeCell);

            return _goal;
        }
    }
}
