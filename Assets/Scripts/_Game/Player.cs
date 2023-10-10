using System;
using _Game;
using _Game.AI;
using Dreamteck.Splines.Primitives;
using Flocking.Behaviours;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour,IKillable
    {
        [FormerlySerializedAs("stayInRadiusBehaviour")] [SerializeField] private GetAwayFromEntity stayInEntityBehaviour;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            stayInEntityBehaviour.radius = GameManager.Instance.GameData.dogChaseRadius;
        }

        private void Update()
        {
            stayInEntityBehaviour.center = transform.position;
            stayInEntityBehaviour.direction = transform.forward;
        }

        public void GetPlayerModel(GameObject model)
        {
            model.transform.parent = transform;
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = quaternion.identity;
            var animator = model.GetComponentInChildren<Animator>();
            GetComponent<PlayerAnimationHandler>().animator = animator;
            _animator =animator;
        }

        public void Die()
        {
            _animator.SetTrigger("die");
            GameManager.Instance.GameOver();
        }
    }
