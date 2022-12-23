using Games.KnightsMove.Scripts.PlayingField;

namespace Games.KnightsMove.Scripts.LevelsControl.Spawners
{
    public struct SpawnerDTO
    {
        public ICellCollection Board { get; set; }
        public IFactoryHolder FactoryHolder { get; set; }
        public IActionHolder ActionHolder { get; set; }
    }
}