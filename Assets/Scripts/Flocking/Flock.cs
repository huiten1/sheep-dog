using System.Collections.Generic;
using System.Linq;
using Spawners;
using Unity.VisualScripting;
using UnityEngine;

namespace Flocking
{
    public class Flock : MonoBehaviour
    {

        public FlockAgent agentPrefab;
        private List<FlockAgent> _agents = new();
        public FlockBehaviour flockBehaviour;

        [SerializeField] private GameObject spawnerObject;
  
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
        
        
        private ISpawner _spawner;
        public float SquareAvoidanceRadius => squareAvoidanceRadius;
        public void RemoveAgent(FlockAgent agent) => _agents.Remove(agent);
        void Start()
        {
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighborRadius = neighborRadius * neighborRadius;
            squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
            
            if(!spawnerObject) return;
            _spawner = spawnerObject.GetComponent<ISpawner>();
            _agents = _spawner.Spawn(agentPrefab,(flockAgent)=>flockAgent.Initialize(this)).ToList();
        }
        // Update is called once per frame
        void Update()
        {
            foreach (FlockAgent agent in _agents)
            {
                if(!agent) continue; 
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


        public void AddToFlock(FlockAgent agent)
        {
            if(!agent) return;
            agent.transform.parent = transform;
            agent.Initialize(this);
            _agents.Add(agent);
        }
    }
}