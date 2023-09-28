using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{ 
    [CreateAssetMenu(menuName = "Flock/Behavior/GetAwayRadius")]
    public class GetAwayFromRadius : FlockBehaviour
    {
        public Vector3 center;
        public float radius;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 centerOffset =  agent.transform.position - center;
            float t = centerOffset.magnitude / radius;
            if (t > 1f)
            {
                return Vector3.zero;
            }

            return centerOffset/ t*t;
        }
    }
}