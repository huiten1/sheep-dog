using DG.Tweening;
using Flocking;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game
{
    public class RewardCalculator : MonoBehaviour
    {
        [SerializeField] private SheepCounter flock;

        [FormerlySerializedAs("rewardSlider")] [SerializeField] private Slider completionSlider;
        [SerializeField] private TMP_Text completionCountText;
        [SerializeField] private TMP_Text rewardText;
        public void CalculateReward()
        {
            completionCountText.SetText($"0 / {flock.StartCount}");
            Invoke(nameof(StartRewardAnim),1f);
        }

        private void StartRewardAnim()
        {
            int capturedFlockCount = flock.enteredSheepCount;
            float completionRate = (float)flock.enteredSheepCount / flock.StartCount;
            completionSlider.DOValue(completionRate, 1f);
            int sheepCount = 0;
            DOTween.To(() => sheepCount, e =>
            {
                sheepCount = e;
                completionCountText.SetText($"{sheepCount} / {flock.StartCount}");
            }, capturedFlockCount, 1f).onComplete += () =>
            {
                int targetReward =  flock.total;
                int dollarAmount = 0;
                DOTween.To(() => sheepCount, e =>
                {
                    dollarAmount = e;
                    rewardText.SetText($"${dollarAmount}");
                }, targetReward, 2f);
            };
        }
    }
}