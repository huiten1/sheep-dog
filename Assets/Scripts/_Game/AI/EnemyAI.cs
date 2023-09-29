using System;
using _Game.Movement;
using Flocking;

using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.AI
{
    public class EnemyAI : MonoBehaviour,IMovementInput
    {
        [SerializeField] private Animator animator;

        [SerializeField] private float period = 3f;

        private float t = 0;

        private void Start()
        {
            GetComponent<Movement.Movement>().Construct(this,new SimpleMotor(transform));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Boid"))
            {
                Attack();    
                // other.gameObject.SetActive(false);
                // other.gameObject.GetComponent<FlockAgent>().RemoveFromFlock();
            }
        }
        void Move(Vector3 direction)
        {
            
        }
        void Attack()
        {
            animator.SetTrigger("attack");
        }

        public MovementData MovementData { get; } = new MovementData();
        public void ReadInput()
        {
            if (t < period)
            {
                t += Time.deltaTime;
                return;
            }
            var unitCircle = Random.insideUnitCircle;
            MovementData.Direction = new Vector3(unitCircle.x, 0, unitCircle.y);
            MovementData.rotation = Quaternion.LookRotation(MovementData.Direction);
            t = 0;
        }
    }
}