using System;
using DG.Tweening;
using UnityEngine;

namespace _Game.FX
{
    public class ScaleFX : MonoBehaviour
    {
        [SerializeField] private float targetScale;
        [SerializeField] private float startScale = 0;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease = Ease.OutBack;

        [SerializeField] private bool startOnEnabled;

        private void OnEnable()
        {
            if(!startOnEnabled) return;
            StartEffect();
        }

        public void StartEffect()
        {
            transform.localScale = Vector3.one * startScale;
            transform.DOScale(targetScale, duration).SetEase(ease);
        }
    }
}