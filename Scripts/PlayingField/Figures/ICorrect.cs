using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures
{
    public interface ICorrect
    {
        void Correct(ICellsControl[] cells);
    }
}
