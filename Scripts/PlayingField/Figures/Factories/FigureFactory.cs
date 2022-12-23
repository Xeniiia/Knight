using Games.KnightsMove.Scripts.PlayingField.Holders;
using Games.KnightsMove.Scripts.PlayingField.Holders.ForFactories;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures.Factories
{
    public class FigureFactory : MonoBehaviour, IFigureFactory
    {
        [SerializeField] private Figure _proto;
        private Figure _figure;


        public IFigure PlaceFigure(Vector2 coordFigure, float sizeCell)
        {
            if (_figure != null)
            {
                _figure.transform.position = coordFigure;
            }
            else
            {
                _figure = Instantiate(_proto, coordFigure, Quaternion.identity, transform);
            }
            _figure.transform.localScale = new Vector2(sizeCell, sizeCell);

            return _figure;
        }

        public IFigure GetFigure() => _figure;
    }
}
