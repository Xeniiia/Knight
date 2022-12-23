using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells.CellsSprites
{
    public class CellSpriteCollection : MonoBehaviour, ICellSpriteCollection
    {
        [SerializeField] private Sprite[] _lightCellSprites;
        [SerializeField] private Sprite[] _lightSelectedCellSprites;
        [SerializeField] private Sprite[] _darkCellSprites;
        [SerializeField] private Sprite[] _darkSelectedCellSprites;

        public int SetRandomSprite(bool color, SpriteRenderer sr)
        {
            int randomNumberOfImage = Random.Range(0, _lightCellSprites.Length);
            sr.sprite = color ? _darkCellSprites[randomNumberOfImage] : _lightCellSprites[randomNumberOfImage];

            return randomNumberOfImage;
        }

        public void SetSelectedCellSprite(bool color, int numOfSprite, SpriteRenderer sr, Vector2 size)
        {
            sr.sprite = color ? _darkSelectedCellSprites[numOfSprite] : _lightSelectedCellSprites[numOfSprite];
            sr.size = size;
            sr.sortingOrder = 1;
        }

        public void SetUnselectedCellSprite(bool color, int numOfSprite, SpriteRenderer sr, Vector2 size)
        {
            sr.sprite = color ? _darkCellSprites[numOfSprite] : _lightCellSprites[numOfSprite];
            sr.size = size;
            sr.sortingOrder = 0;
        }
    }
}
