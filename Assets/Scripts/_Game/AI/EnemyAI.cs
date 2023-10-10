using System;
using _Game.Movement;
using _Game.Movement.Motors;
using Flocking;

using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.AI
{
    public class EnemyAI : MonoBehaviour,IMovementInput
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private float fov = 60;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float moveSpeed;

        [SerializeField] private Collider confineBox;
        [SerializeField] private Transform[] patrolPoints;

        [SerializeField] private LayerMask _layerMask;
        private float cooldown_t;
        
        private Transform target;
        private IKillable _killable;
        private void Start()
        {
            EvaluateNextMove();
            GetComponent<Movement.Movement>().Construct(this,new RbMotor(GetComponent<Rigidbody>(),moveSpeed));
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1<<other.gameObject.layer | _layerMask) ==0) return;
            
            target = other.transform;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!(Vector3.Angle(transform.forward, (other.transform.position - transform.position)) < fov)) return;

            if (other.gameObject.GetComponent<IKillable>() == null) return;
            _killable = other.gameObject.GetComponent<IKillable>();
            if(Time.time-cooldown_t>attackCooldown)    
                Attack(_killable);
        }

        private void Update()
        {
            if(!target) return;
            if(Vector3.Distance(target.position,transform.position)>1f) return;
            EvaluateNextMove();
        }
        
        void Attack(IKillable killableEntity)
        {
            cooldown_t = Time.time;
            animator.SetTrigger("attack");
            killableEntity.Die();
            _killable = null;
        }

        public MovementData MovementData { get; } = new MovementData();

        public void ReadInput()
        {
            if (!confineBox.bounds.Contains(transform.position))
            {
                _killable = null;
                EvaluateNextMove();
            } 

            var moveDir =( target.position - transform.position).normalized ;
            MovementData.Direction = moveDir;
            if(moveDir.magnitude>0.01f)
                MovementData.rotation =  Quaternion.LookRotation(MovementData.Direction);
         
        }

        private void EvaluateNextMove()
        {
            target = _killable!=null? target.transform: patrolPoints[Random.Range(0, patrolPoints.Length)];
        }
    }
}