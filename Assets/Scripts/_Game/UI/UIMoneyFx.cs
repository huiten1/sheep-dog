using System;
using DG.Tweening;
using Dreamteck.Splines.Primitives;
using UnityEngine;
using Utils;

using System.Threading.Tasks;
namespace _Game.UI
{
    public class UIMoneyFx : MonoBehaviour
    {
        [SerializeField] private float duration = 1f;
        [SerializeField] public Vector3 offset;
        [SerializeField] public float punchScaleAmount;
        private async void OnEnable()
        {
            await Task.Yield();
            PlayAnim();
        }
        
        private void PlayAnim()
        {
            var seq = DOTween.Sequence();
            seq.Append(transform.DOMove(transform.position + offset, duration));
            seq.Append(transform.DOPunchScale(punchScaleAmount * Vector3.one, 0.3f, 1, 1));
            seq.onComplete += () => GetComponent<ReturnToPool>().Release();
        }
    }
}