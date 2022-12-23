using System;
using System.Collections;
using Games.KnightsMove.Scripts.LevelsControl.Spawners;
using Games.KnightsMove.Scripts.PlayingField;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Games.KnightsMove.Scripts.LevelsControl.CheckListManager.CheckListElements.Rewards
{
    public enum RewardName
    {
        GoldenStand,
        GoldenKnight,
        GoldenCrown
    }
    
    public class Reward : CheckListElement
    {
        [SerializeField] private Sprite achievementSprite;
        [SerializeField] private GameObject rewardView;
        [SerializeField] private GameObject fade;
        [SerializeField] private ParticleSystem boomParticles;
        [SerializeField] private RewardName rewardName;
        private Sprite _previousSprite;
        private Image _rewardViewArea;
        private Image _fadeImage;


        public Sprite GetRewardSprite() => achievementSprite;

        public string GetRewardName()
        {
            string localizedTask = "";
            if (rewardName == RewardName.GoldenStand)
                localizedTask = LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "StartGamePopup_StepsGame_2");
            else if (rewardName == RewardName.GoldenKnight)
                localizedTask = LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "StartGamePopup_StepsGame_3");
            else if (rewardName == RewardName.GoldenCrown)
                localizedTask = LocalizationSettings.StringDatabase.GetLocalizedString("Games_KnightsMove", "StartGamePopup_StepsGame_4");

            return localizedTask;
        }


        private void Awake()
        {
            _rewardViewArea = rewardView.GetComponent<Image>();
            _fadeImage = fade.GetComponent<Image>();
        }


        public override LevelInfoDTO Execute(SpawnerDTO spawnDto)
        {
            //StartCoroutine(ViewReward());
            var figureFactory = spawnDto.FactoryHolder.GetFigureFactory();
            var figure = figureFactory.GetFigure();
            _previousSprite = figure.ChangeSprite(achievementSprite);

            return new LevelInfoDTO
            {
                Level = null
            };
        }

        private IEnumerator ViewReward()
        {
            var fadeImageColor = _fadeImage.color;
            fadeImageColor.a = 0;
            _fadeImage.color = fadeImageColor;
            fade.SetActive(true);
            yield return SlowImageAlphaChange(_fadeImage, 0, 0.6f, 1f);
            rewardView.SetActive(true);
            _rewardViewArea.sprite = achievementSprite;
            yield return SlowImageAlphaChange(_rewardViewArea, 0, 1, 0.3f);
            boomParticles.Play();
            yield return new WaitForSeconds(2f);
            yield return SlowImageAlphaChange(_rewardViewArea, 1, 0, 0.3f);
            yield return SlowImageAlphaChange(_fadeImage, 0.6f, 0, 0.3f);
            _rewardViewArea.sprite = null;
            rewardView.SetActive(false);
            fade.SetActive(false);
            fadeImageColor = _fadeImage.color;
            fadeImageColor.a = 100;
            _fadeImage.color = fadeImageColor;
            
            
            yield return null;
            
        }

        private IEnumerator SlowImageAlphaChange(Image image, float from, float to, float duration)
        {
            float time = 0;
            while (time <= duration)
            {
                time += Time.deltaTime;
                var fadeImageColor = image.color;
                fadeImageColor.a = Mathf.Lerp(from, to, time / duration);
                image.color = fadeImageColor;
                yield return null;
            }
        }

        public override void ExecuteUndo(IFigure figure)
        {
            figure.ChangeSprite(_previousSprite);
        }
    }
}