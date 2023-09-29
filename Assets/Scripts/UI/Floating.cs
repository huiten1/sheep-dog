using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Floating : MonoBehaviour
    {
        public Vector3 offset;
        public float duration;

        private void Start()
        {
            transform.DOMove(transform.position + offset, duration);
        }
    }
}