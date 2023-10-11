using System;
using UnityEngine;

namespace _Game
{
    public class ActivateColliderNearGameObject : MonoBehaviour
    {
        [SerializeField] float radius;
        [SerializeField] private GameObject target;
        [SerializeField] private Collider targetCollider;

        private float _sqrRadius;

        private void Awake()
        {
            _sqrRadius = radius * radius;
        }

        private void Update()
        {
            if (Vector3.SqrMagnitude(target.transform.position - targetCollider.transform.position) < _sqrRadius)
            {
                targetCollider.enabled=true;
                // targetCollider.gameObject.layer = 0;
            }
            else
            {
                targetCollider.enabled=false;
                // targetCollider.gameObject.layer = 6;
            }
        }
    }
}