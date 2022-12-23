using System;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.Holders;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.CellsItems.Pits.Factories
{
    public class PitFactory : MonoBehaviour, IPitFactory
    {
        [SerializeField] private Pit proto;
        private Pit _pit;


        public ICellItem GetPit(Vector2 pos, float sizeCell, Action stepAction)
        {
            _pit = Instantiate(proto, pos, Quaternion.identity, transform);
            _pit.transform.localScale = new Vector2(sizeCell, sizeCell);
            _pit.PitTriggeredAction += stepAction;

            return _pit;
        }
    }
}