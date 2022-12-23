namespace Games.KnightsMove.Scripts.PlayingField.Holders.ForFactories
{
    public class FactoryHolder : IFactoryHolder
    {
        private readonly IFigureFactory _figureFactory;
        private readonly IGoalFactory _goalFactory;
        private readonly IPitFactory _pitFactory;
        private readonly IStormFactory _stormFactory;

        public FactoryHolder(IFigureFactory figureFactory, IGoalFactory goalFactory, IPitFactory pitFactory,
            IStormFactory stormFactory)
        {
            _figureFactory = figureFactory;
            _goalFactory = goalFactory;
            _pitFactory = pitFactory;
            _stormFactory = stormFactory;
        }


        public IFigureFactory GetFigureFactory() => _figureFactory;


        public IGoalFactory GetGoalFactory() => _goalFactory;


        public IPitFactory GetPitFactory() => _pitFactory;


        public IStormFactory GetStormFactory() => _stormFactory;
    }
}