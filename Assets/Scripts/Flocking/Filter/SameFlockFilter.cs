using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Filter
{
    [CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
    public class SameFlockFilter : ContextFilter
    {
        readonly List<Transform> _filtered = new ();
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            _filtered.Clear();
            foreach (Transform item in original)
            {
                FlockAgent itemAgent = item.GetComponent<FlockAgent>();
                if (itemAgent  && itemAgent.AgentFlock == agent.AgentFlock)
                {
                    _filtered.Add(item);
                }
            }
            return _filtered;
        }
    }
}