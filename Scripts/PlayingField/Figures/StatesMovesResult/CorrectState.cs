using System;
using System.Linq;
using Games.KnightsMove.Scripts.CoroutineExtension;
using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures.StatesMovesResult
{
    public class CorrectState : ICellBehaviour
    {
        private readonly Func<Vector2[], (CoroutineSequence, CoroutineSequence)> _moveFigure;


        public CorrectState(Func<Vector2[], (CoroutineSequence, CoroutineSequence)> action)
        {
            _moveFigure = action;
        }


        public bool EffectSelectedCell(ICellCollection board, ICellsControl[] cells, out CoroutineSequence lastCoroutine, out CoroutineSequence firstCoroutine)
        {
            (board as ICorrect)?.Correct(cells);
            var positions = cells.Select(x => x.Position).ToArray();
            var coroutines = _moveFigure(positions);
            lastCoroutine = coroutines.Item1;
            firstCoroutine = coroutines.Item2;

            return true;
        }
    }
}
