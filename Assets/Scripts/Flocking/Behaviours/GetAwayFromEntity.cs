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
        public float scale = 1;
        public float drive = 1;
        public float radius;
        public float angle;
        public float minSpeed;
        public float maxSpeed;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 centerOffset =  agent.transform.position - center;
            float t =  centerOffset.magnitude / radius ;
            if (t > 1f)
            {
                return Vector3.zero;
            }
            if (Vector3.Angle(direction.normalized,centerOffset.normalized) > angle  )
            {
                return Vector3.zero;
            }

            return direction * Mathf.Lerp(maxSpeed, minSpeed, t);
            // return  direction * ((1+drive) * getAwayCurve.Evaluate(t)*scale) + centerOffset ;
        }
    }
}