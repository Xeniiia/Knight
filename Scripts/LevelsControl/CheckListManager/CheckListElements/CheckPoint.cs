using System.Collections;
using UnityEngine;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements
{
    public enum PointType
    {
        Level,
        Reward
    }
    
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private ParticleSystem onAchievedParticles;
        [SerializeField] private Sprite achievedPoint;
        [SerializeField] private SpriteRenderer itemAreaOnPoint;
        [SerializeField] private SpriteRenderer onAchievedSprite;
        [SerializeField] private SpriteRenderer lightEffect;
        [SerializeField] private PointType type;
        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        public PointType GetCheckPointType() => type;
        
        
        public void SetLevelSprite(Sprite levelSprite)
        {
            itemAreaOnPoint.sprite = levelSprite;
        }


        public Sprite GetLevelSprite() => itemAreaOnPoint.sprite;

        
        public void MarkAsReceived()
        {
            StartCoroutine(ShinePoint());
            _spriteRenderer.sprite = achievedPoint;
        }

        private IEnumerator ShinePoint()
        {
            onAchievedParticles.Play();
            //Debug.Log(Time.time);
            yield return AlphaChange(new[] { lightEffect, onAchievedSprite }, 0.5f, 0, 100);
            //Debug.Log(Time.time);
            yield return AlphaChange(new[] { lightEffect, onAchievedSprite }, 0.5f, 100, 0);

            yield return null;
        }

        private static IEnumerator AlphaChange(SpriteRenderer[] hiddenObjects, float duration, float from, float to)
        {
            float time = 0;
            while (time <= duration)
            {
                time += Time.deltaTime;
                foreach (var spriteRenderer in hiddenObjects)
                {
                    var color = spriteRenderer.color;
                    color.a = Mathf.Lerp(from, to, time / duration);
                    spriteRenderer.color = color;
                }
                yield return null;
            }
            yield return null;
        }


        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}