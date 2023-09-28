using System;
using Flocking.Behaviours;
using UnityEngine;

namespace Flocking
{
    public class StayInBoxAssigner : MonoBehaviour
    {
        [SerializeField] private StayInBoxBehaviour stayInBoxBehaviour;

        private void Awake()
        {
            stayInBoxBehaviour.center = transform.position;
            stayInBoxBehaviour.size = transform.localScale;
        }
    }
}