using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Game
{
    public class GameStateChangeListener : MonoBehaviour
    {
        [SerializeField] private GameState targetState;
        [FormerlySerializedAs("onStateActivated")] public UnityEvent<GameState> onStateIsTargetState;
        public UnityEvent<GameState> onStateNotTargetState;
        [SerializeField] public float delay;
        private void Awake()
        {
            GameManager.OnStateChange += HandleStateChange;
            HandleStateChange(GameManager.Instance.State);
        }
        
        
        private void OnDestroy()
        {
            GameManager.OnStateChange -= HandleStateChange;
        }

        private void HandleStateChange(GameState state)
        {
            
            if (state == targetState)
            {
                if (delay > 0)
                {
                    float t = 0;
                    DOTween.To(() => t, x => t = x, 1f,delay).onComplete+=()=> onStateIsTargetState?.Invoke(state);
                }
                else
                {
                    onStateIsTargetState?.Invoke(state);
                }
            }
            else
            {
                onStateNotTargetState?.Invoke(state);
            }
        }

     
    }
}