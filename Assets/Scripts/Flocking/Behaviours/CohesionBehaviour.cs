﻿using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
    public class CohesionBehaviour : FilteredFlockBehaviour
    {
        private List<Transform> filteredContext = new();
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
                return Vector3.zero;

            //add all points together and average
            Vector3 cohesionMove = Vector3.zero;
            filteredContext   = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                cohesionMove += item.position;
            }
            cohesionMove /= context.Count;

            //create offset from agent position
            cohesionMove -= agent.transform.position;
            return cohesionMove;
        }
    }
}