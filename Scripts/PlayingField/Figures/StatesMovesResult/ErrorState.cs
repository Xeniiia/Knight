using Games.KnightsMove.Scripts.CoroutineExtension;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures.StatesMovesResult
{
    [System.Serializable]
    public class ErrorState : ICellBehaviour
    {
        public bool EffectSelectedCell(ICellCollection board, ICellsControl[] cells, out CoroutineSequence lastCoroutine, out CoroutineSequence firstCoroutine)
        {
            (board as IError)?.Error(cells);
            lastCoroutine = null;
            firstCoroutine = null;

            return false;
        }
    }
}
