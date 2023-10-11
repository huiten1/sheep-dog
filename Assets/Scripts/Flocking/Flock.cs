using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Indicator;
using Spawners;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Flocking
{
    public class Flock : MonoBehaviour, IIndicator<float>
    {

        public FlockAgent agentPrefab;
        public FlockAgent goldenSheep;
        private List<FlockAgent> _agents = new();
        public FlockBehaviour flockBehaviour;
        [SerializeField] private LayerMask checkLayers;

        [SerializeField] private GameObject spawnerObject;
        [SerializeField] private GameObject goldenSheepSpawner;
  
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

        private const int MaxColliders = 100;
        private Collider[] _getNearbyResults = new Collider[MaxColliders];
        private readonly List<Transform> _context = new();
        private ISpawner _spawner;
        public float SquareAvoidanceRadius => squareAvoidanceRadius;
        public void RemoveAgent(FlockAgent agent) => _agents.Remove(agent);

        public  int startFlockCount;
        public int FlockCount => _agents.Count;
        private int _prevCount = 0;
        void Start()
        {
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighborRadius = neighborRadius * neighborRadius;
            squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
            
            if(!spawnerObject) return;
            _spawner = spawnerObject.GetComponent<ISpawner>();
            _agents = _spawner.Spawn(agentPrefab,(flockAgent)=>flockAgent.Initialize(this)).ToList();
            
            _spawner = goldenSheepSpawner.GetComponent<ISpawner>();
            _agents=_agents.Concat( _spawner.Spawn(goldenSheep,(flockAgent)=>flockAgent.Initialize(this))).ToList();
            
            startFlockCount = _agents.Count;
       
            float duration = GameManager.Instance.GameData.levelTime;

           
            DOTween.To(() => driveFactor, x => driveFactor = x, 6, duration);
        }

 
        void Update()
        {
            foreach (FlockAgent agent in _agents)
            {
                if(!agent) continue; 
                GetNearbyObjects(agent);

                Vector3 move = flockBehaviour.CalculateMove(agent, _context, this);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
                agent.Move(move);
            }

            if (_agents.Count == _prevCount) return;
            _prevCount = _agents.Count;
            OnValueChanged?.Invoke();
        }
        
        void GetNearbyObjects(FlockAgent agent)
        {
            _context.Clear();
            var count = Physics.OverlapSphereNonAlloc(agent.transform.position, neighborRadius, _getNearbyResults,checkLayers);
            for (int i = 0; i < count; i++)
            {
                if (_getNearbyResults[i] != agent.AgentCollider)
                {
                    _context.Add(_getNearbyResults[i].transform);
                }
            }
        }
        public void AddToFlock(FlockAgent agent)
        {
            if(!agent) return;
            agent.transform.parent = transform;
            agent.Initialize(this);
            _agents.Add(agent);
        }

        public float Value
        {
            get
            {
                if (startFlockCount == 0) return 0;
                var agentsCount =1f-(float) _agents.Count / startFlockCount;
                return agentsCount;
            }
        }

        public event Action OnValueChanged;
    }
}