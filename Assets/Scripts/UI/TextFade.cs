using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextFade : Fade
    {
        [SerializeField] private TMP_Text text;
        public override void StartFade()
        {
            text.DOFade(endValue, duration);
        }
    }
}