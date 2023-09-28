using System;
using _Game.Movement;
using UnityEngine;

namespace Flocking
{
    public class FlockAgent : MonoBehaviour,IMovementInput
    {
        private Flock agentFlock;
        public Flock AgentFlock => agentFlock;

        private Collider agentCollider;
        public Collider AgentCollider => agentCollider;
        private Vector3 _vel;
        public Vector3 Velocity => _vel;
        private void Start()
        {
            agentCollider = GetComponent<Collider>();
        }

        public void Initialize(Flock flock)
        {
            agentFlock = flock;
        }

        public void Move(Vector3 vel)
        {
            vel.y = 0;
            _vel = vel;
            if(vel.magnitude>0.001f)
                MovementData.rotation = Quaternion.LookRotation(vel);
            MovementData.Direction = vel;
            if(vel.magnitude>0.001f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vel),Time.deltaTime*5);
            transform.position += vel * Time.deltaTime;
        }

        public MovementData MovementData { get; private set; } = new MovementData();
        public void ReadInput()
        {
            
        }
    }
}