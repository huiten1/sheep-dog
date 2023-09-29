using System;
using Flocking.Behaviours;
using UnityEngine;

namespace Flocking
{
    public class StayInRadiusAssigner : MonoBehaviour
    {
        [SerializeField] private StayInRadiusBehaviour stayInRadiusBehaviour;

        private void Awake()
        {
            stayInRadiusBehaviour.center = transform.position;
            stayInRadiusBehaviour.radius = transform.localScale.magnitude;
        }

        private void Update()
        {
            if (stayInRadiusBehaviour.center == transform.position) return;
            stayInRadiusBehaviour.center = transform.position;
            stayInRadiusBehaviour.radius = transform.localScale.magnitude;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position,transform.localScale.magnitude);
        }
    }
}