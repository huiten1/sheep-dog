using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class Fade : MonoBehaviour
    {
        public float endValue;
        public float duration;
        private void Start()
        {
            StartFade();
        }

        public abstract void StartFade();
    }
}