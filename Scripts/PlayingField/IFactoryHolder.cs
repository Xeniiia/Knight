using Games.KnightsMove.Scripts.PlayingField.Holders;
using Games.KnightsMove.Scripts.PlayingField.Holders.ForFactories;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface IFactoryHolder
    {
        IFigureFactory GetFigureFactory();
        IGoalFactory GetGoalFactory();
        IPitFactory GetPitFactory();
        IStormFactory GetStormFactory();
    }
}