using Games.KnightsMove.Scripts.PlayingField.Board;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures
{
    public interface IError
    {
        void Error(ICellsControl[] cells);
    }
}
