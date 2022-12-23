using Games.KnightsMove.Scripts.PlayingField;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Games.KnightsMove.Scripts.UI
{
    public class CurrentNumOfMovesUI : MonoBehaviour
    {
        [SerializeField] private GameObject numOfMovesTable;
        [SerializeField] private Image numArea;
        [SerializeField] private Sprite[] numberSprites;
        private IPlayingField _playingField;

        private void Awake()
        {
            _playingField = GetComponent<IPlayingField>();
        }

        private void Start()
        {
            _playingField.UpdateNumOfMoves += UpdateText;
        }

        private void UpdateText(int newValue)
        {
            if (newValue != -1)
            {
                numOfMovesTable.SetActive(true);
                numArea.sprite = numberSprites[newValue];
            }
            else
            {
                numOfMovesTable.SetActive(false);
            }
        }

    }
}
