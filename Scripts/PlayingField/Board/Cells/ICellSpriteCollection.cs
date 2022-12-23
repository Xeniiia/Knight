using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells
{
    public interface ICellSpriteCollection
    {
        int SetRandomSprite(bool color, SpriteRenderer sr);
        void SetSelectedCellSprite(bool color, int numOfSprite, SpriteRenderer sr, Vector2 size);
        void SetUnselectedCellSprite(bool color, int numOfSprite, SpriteRenderer sr, Vector2 size);
    }
}
