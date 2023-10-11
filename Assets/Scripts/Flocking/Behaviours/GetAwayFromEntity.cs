using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Behaviours
{ 
    [CreateAssetMenu(menuName = "Flock/Behavior/GetAwayFrom Entity")]
    public class GetAwayFromEntity : FlockBehaviour
    {
        [SerializeField]
        private AnimationCurve getAwayCurve;
        public Vector3 center;
        public Vector3 direction;
        public float drive = 1;
        public float radius;
        public float angle;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 centerOffset =  agent.transform.position - center;
            float t = 2* centerOffset.magnitude / radius ;
            if (t > 2f)
            {
                return Vector3.zero;
            }
            if (Vector3.Angle(direction.normalized,centerOffset.normalized) > angle  )
            {
                return Vector3.zero;
            }
            
            return direction * ((1+drive) * getAwayCurve.Evaluate(t)) + centerOffset ;
        }
    }
}