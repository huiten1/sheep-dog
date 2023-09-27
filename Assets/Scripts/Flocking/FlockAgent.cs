using System;
using UnityEngine;

namespace Flocking
{
    public class FlockAgent : MonoBehaviour
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
            _vel = vel;
            if(vel.magnitude>0.001f)
                transform.rotation = Quaternion.LookRotation(vel);
            transform.position += vel * Time.deltaTime;
        }
    }
}