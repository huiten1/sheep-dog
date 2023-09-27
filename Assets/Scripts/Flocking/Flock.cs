using System.Collections.Generic;
using UnityEngine;

namespace Flocking
{
    public class Flock : MonoBehaviour
    {

        public FlockAgent agentPrefab;
        private List<FlockAgent> _agents = new();
        public FlockBehaviour flockBehaviour;

        public int startingCount = 100;
         const float AgentDensity = 1f;
  
        [Range(1f, 100f)]
        public float driveFactor = 10f;
        [Range(1f, 100f)]
        public float maxSpeed = 5f;
        [Range(1f, 10f)]
        public float neighborRadius = 1.5f;
        [Range(0f, 1f)]
        public float avoidanceRadiusMultiplier = 0.5f;
        
        float squareMaxSpeed;
        float squareNeighborRadius;
        float squareAvoidanceRadius;
        public float SquareAvoidanceRadius => squareAvoidanceRadius;
        
        void Start()
        {
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighborRadius = neighborRadius * neighborRadius;
            squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

            for (int i = 0; i < startingCount; i++)
            {
                FlockAgent newAgent = Instantiate(
                    agentPrefab,
                     Vector3.ProjectOnPlane(Random.insideUnitSphere,Vector3.up) * startingCount * AgentDensity,
                    Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                    transform
                );
                newAgent.name = "Agent " + i;
                newAgent.Initialize(this);
                _agents.Add(newAgent);
            }
        }
        // Update is called once per frame
        void Update()
        {
            foreach (FlockAgent agent in _agents)
            {
                List<Transform> context = GetNearbyObjects(agent);

                Vector3 move = flockBehaviour.CalculateMove(agent, context, this);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
                agent.Move(move);
            }
        }
        
        List<Transform> GetNearbyObjects(FlockAgent agent)
        {
            List<Transform> context = new List<Transform>();
            Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
            foreach (var c in contextColliders)
            {
                if (c != agent.AgentCollider)
                {
                    context.Add(c.transform);
                }
            }
            return context;
        }


    }
}