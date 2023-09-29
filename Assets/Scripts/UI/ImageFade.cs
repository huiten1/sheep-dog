using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageFade : Fade
    {
        [SerializeField] private Image _image;
        public override void StartFade()
        {
            _image.DOFade(endValue, duration);
        }
    }
}