using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/StayInRadius")]
    public class StayInRadiusBehaviour : FilteredFlockBehaviour
    {
        public Vector3 center;
        public float radius = 15f;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 centerOffset = center - (Vector3)agent.transform.position;
            float t = centerOffset.magnitude / radius;
            if (t < 0.9f)
            {
                return Vector3.zero;
            }

            return centerOffset * (t * t);
        }
    }
}