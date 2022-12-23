using System;
using System.Collections;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.CellsItems.Storms
{
    public class Storm : MonoBehaviour, ICellItem
    {
        public event Action StormTriggeredAction;


        public void Execute()
        {
            StormTriggeredAction?.Invoke();
        }
        

        public void DestroySelf()
        {
            StartCoroutine(Destroying());
        }


        private IEnumerator Destroying()
        {
            yield return SlowScaleChange(transform, transform.localScale.x, 0, 0.5f);
            Destroy(gameObject);
        }
        
        
        private IEnumerator SlowScaleChange(Transform tr, float from, float to, float duration)
        {
            float time = 0;
            while (time <= duration)
            {
                time += Time.deltaTime;
                var trLocalScale = tr.localScale;
                trLocalScale.x = Mathf.Lerp(from, to, time / duration);
                trLocalScale.y = Mathf.Lerp(from, to, time / duration);
                tr.localScale = trLocalScale;
                yield return null;
            }
        }
    }
}