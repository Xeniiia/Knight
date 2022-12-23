using System;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;

namespace Games.KnightsMove.Scripts.PlayingField.Holders.ForActions
{
    public class ActionHolder : IActionHolder
    {
        private readonly Action _winAction;
        private readonly Action _pitAction;
        private readonly Action _stormAction;

        public ActionHolder(Action winAction, Action pitAction, Action stormAction)
        {
            _winAction = winAction;
            _pitAction = pitAction;
            _stormAction = stormAction;
        }


        public Action GetWinAction() => _winAction;
        
        
        public Action GetPitAction() => _pitAction;
        
        
        public Action GetStormAction() => _stormAction;
    }
}