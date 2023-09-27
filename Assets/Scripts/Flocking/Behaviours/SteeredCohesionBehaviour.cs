using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
    public class SteeredCohesionBehaviour : FilteredFlockBehaviour
    {
        Vector3 _currentVelocity;
        public float agentSmoothTime = 0.5f;

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            //if no neighbors, return no adjustment
            if (context.Count == 0)
                return Vector3.zero;

            //add all points together and average
            Vector3 cohesionMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                cohesionMove += item.position;
            }
            cohesionMove /= context.Count;

            //create offset from agent position
            cohesionMove -= agent.transform.position;
            cohesionMove = Vector3.SmoothDamp(agent.Velocity, cohesionMove, ref _currentVelocity, agentSmoothTime);
            return cohesionMove;
        }
    }
}