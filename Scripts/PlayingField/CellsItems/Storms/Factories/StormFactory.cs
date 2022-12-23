using System;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.Holders;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.CellsItems.Storms.Factories
{
    public class StormFactory : MonoBehaviour, IStormFactory
    {
        [SerializeField] private Storm proto;
        private Storm _storm;


        public ICellItem GetStorm(Vector2 pos, float sizeCell, Action stepAction)
        {
            _storm = Instantiate(proto, pos, Quaternion.identity, transform);
            _storm.transform.localScale = new Vector2(sizeCell * 0.5f, sizeCell * 0.5f);
            _storm.StormTriggeredAction += stepAction;

            return _storm;
        }
    }
}