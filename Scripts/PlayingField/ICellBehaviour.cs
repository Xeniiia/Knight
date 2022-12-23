using Games.KnightsMove.Scripts.CoroutineExtension;
using Games.KnightsMove.Scripts.PlayingField.Board;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface ICellBehaviour
    {
        bool EffectSelectedCell(ICellCollection board, ICellsControl[] cells, out CoroutineSequence coroutine, out CoroutineSequence firstCoroutine);
    }
}
