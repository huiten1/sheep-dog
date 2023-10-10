using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/StayInBox")]
    public class StayInBoxBehaviour : FilteredFlockBehaviour
    {
        public  Vector3 center;
        public Vector3 size;
        [SerializeField] private float dist;
        private Bounds _bound;
        
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            _bound = new Bounds(center, size);

            var position = agent.transform.position;
            Vector3 centerOffset = _bound.ClosestPoint(position) - position;
            float t = centerOffset.magnitude / dist;
            
            if (t < 0.9f && _bound.Contains(position))
            {
                return Vector3.zero;
            }
            return centerOffset * (t * t);
        }
    }
}