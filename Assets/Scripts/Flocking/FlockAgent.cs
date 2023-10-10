using System;
using _Game.Movement;
using Cinemachine.Utility;
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
            if(vel.IsNaN()) return;
            vel.y = 0;
            _vel = vel;
            if(vel.magnitude>0.001f)
                MovementData.rotation = Quaternion.LookRotation(vel);
            MovementData.Direction = vel / 3f;
            if(vel.magnitude>0.1f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vel),Time.deltaTime*2);
            transform.position += vel * Time.deltaTime;
        }


        public void RemoveFromFlock() => agentFlock.RemoveAgent(this);
        public MovementData MovementData { get; private set; } = new MovementData();
        public void ReadInput(){}
    }
}