using System;
using Indicator;
using UnityEngine;
using UnityEngine.Events;

namespace _Game
{
    public class Timer : MonoBehaviour,IIndicator<float>,IIndicator<string>
    {
        public float time;
        private float _t;
        public float CurrentTime => _t;
        [SerializeField] private bool countDown;
        public UnityEvent onTimerComplete;
        private bool _complete;
      

        private void Start()
        {
            time = GameManager.Instance.GameData.levelTime;
            if (countDown) _t = time;
        }

        private void Update()
        {
            if(_complete) return;

            if (countDown)
            {
                if (_t > 0)
                {
                    _t -= Time.deltaTime;
                    OnValueChanged?.Invoke();
                    return;
                }
            }
            else
            {
                if (_t < time)
                {
                    _t += Time.deltaTime;
                    OnValueChanged?.Invoke();
                    return;
                }
            }
           
            onTimerComplete?.Invoke();
            GameManager.Instance.Complete();
            _complete = true;
        }
        public float Value => _t / time;

        string IIndicator<string>.Value => CurrentTime.ToString("F0");

        public event Action OnValueChanged;
    }
}