using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
    public class AlignmentBehaviour : FilteredFlockBehaviour
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
                return Vector3.zero;
            
            Vector3 alignmentMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                alignmentMove += item.transform.forward;
            }
            alignmentMove /= context.Count;

            return alignmentMove;
        }
    }
}