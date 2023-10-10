using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
    public class AvoidanceBehaviour : FilteredFlockBehaviour
    {
        private List<Transform> _filteredContext = new();
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
                return Vector3.zero;

            //add all points together and average
            Vector3 avoidanceMove = Vector3.zero;
            int nAvoid = 0;
            _filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in _filteredContext)
            {
                if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    nAvoid++;
                    var dir = agent.transform.position - item.position;
                    float t = (dir.sqrMagnitude / flock.SquareAvoidanceRadius);
                    avoidanceMove += dir.normalized/(t*t);
                }
            }
            if (nAvoid > 0)
                avoidanceMove /= nAvoid;

            return avoidanceMove;
        }
    }
}