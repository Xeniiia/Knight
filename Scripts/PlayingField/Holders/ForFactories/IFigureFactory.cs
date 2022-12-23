using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Holders.ForFactories
{
    public interface IFigureFactory
    {
        IFigure PlaceFigure(Vector2 coordFigure, float sizeCell);
        IFigure GetFigure();
    }
}
