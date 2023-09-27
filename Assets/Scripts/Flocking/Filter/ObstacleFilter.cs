using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Filter
{
    [CreateAssetMenu(menuName = "Flock/Filter/Obstacles")]
    public class ObstacleFilter : ContextFilter
    {
        [SerializeField] private Transform[] obstacles;
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            List<Transform> filtered = new List<Transform>();
            foreach (Transform item in original)
            {
                FlockAgent itemAgent = item.GetComponent<FlockAgent>();
                if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}