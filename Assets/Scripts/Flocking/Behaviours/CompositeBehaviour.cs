using System;
using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
    public class CompositeBehaviour : FlockBehaviour
    {
        
        [Serializable]
        public struct FlockBehaviourWeights
        {
            public FlockBehaviour behavior;
            public float weight;
        }

        public FlockBehaviourWeights[] behaviours;
        // public FlockBehaviour[] behaviors;
        // public float[] weights;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {

            Vector3 move = Vector3.zero;

            //iterate through behaviors
            for (int i = 0; i < behaviours.Length; i++)
            {
                float weight = behaviours[i].weight;
                Vector3 partialMove = behaviours[i].behavior.CalculateMove(agent, context, flock) * weight ;

                if (partialMove != Vector3.zero)
                {
                    if (partialMove.sqrMagnitude > weight*weight)
                    {
                        partialMove.Normalize();
                        partialMove *= weight;
                    }

                    move += partialMove;

                }
            }

            return move;
        }
    }
}