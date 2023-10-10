using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Filter
{
    [CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
    public class PhysicsLayerFilter : ContextFilter
    {
        public LayerMask mask;
        private readonly List<Transform> _filtered = new();
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            _filtered.Clear();
            foreach (Transform item in original)
            {
                if (mask == (mask | (1 << item.gameObject.layer)))
                {
                    _filtered.Add(item);
                }
            }
            return _filtered;
        }
    }
}