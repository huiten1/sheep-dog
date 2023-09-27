
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Boids
{
    public class BoidAgent : MonoBehaviour
    {
        private Transform fleeTarget;

        [Header("Seek")] 
        [SerializeField] private Transform seekTarget;
        [SerializeField] BoidRules _boidRules;

        public Vector3 Velocity { get; private set; }
        
        public BoidRules BoidRules
        {
            get { return _boidRules; }
        }
        const int MaxColliders = 20;
        private Collider[] colliders = new Collider[MaxColliders];
        private void Start()
        {
   
            fleeTarget = FindObjectOfType<Player>().transform;

            Velocity = Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up).normalized;
        }

        private void Update()
        {
            
            var length = Physics.OverlapSphereNonAlloc(transform.position, _boidRules.radius, colliders, 1<<6);
            // Debug.Log(colliders.Length);
            if(length==0) return;
            // Debug.Log("boid agent");
            var neighbours = colliders.Where(e=>e).Select(e => e.transform).ToArray();
            
            var vel = Velocity;
            if(fleeTarget)
                vel += Flee(fleeTarget.position,_boidRules.fleeWeight);
            vel += Cohesion(neighbours, BoidRules.cohesionSteps, BoidRules.cohesionWeight);
            vel += Seperation(neighbours, BoidRules.seperationSteps, BoidRules.seperationWeight);
            vel += Alignment(neighbours, BoidRules.alignmentWeight);
            vel += Seek(seekTarget? seekTarget.position:Vector3.zero,_boidRules.seekWeight);
            vel = Vector3.ProjectOnPlane(vel, Vector3.up);
            vel = Vector3.ClampMagnitude(vel,_boidRules.maxSpeed);

            // rb.velocity = vel;
            // rb.MovePosition(rb.position+vel*Time.deltaTime);
            transform.position += vel * Time.deltaTime;
            Velocity = vel;
            
            if (vel.sqrMagnitude > 0.0001f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vel), Time.deltaTime * 5);
            // rb.MoveRotation( Quaternion.Slerp(rb.rotation,Quaternion.LookRotation(vel),Time.deltaTime*5));
        }

       
        Vector3 Seek(Vector3 target, float weight)
        {
            if (Mathf.Abs(weight) < 0.00001f) return Vector3.zero;

            var desiredVelocity = Vector3.Normalize(target - transform.position) * weight;
            return desiredVelocity - Velocity;
        }
        
        Vector3 Flee(Vector3 target, float weight)
        {
            if (Mathf.Abs(weight) < 0.00001f) return Vector3.zero;
            
            float dist = (transform.position - target).magnitude;
            // if (dist > _boidRules.radius) return Vector3.zero;
            var desiredVelocity = Vector3.Normalize( transform.position - target) / dist * weight;
            return desiredVelocity - Velocity;
        }

        Vector3 Cohesion(Transform[] neighbours,float steps, float weight)
        {
            var percievedCenter = transform.position;
            for (int i = 0; i < neighbours.Length; i++)
            {
                percievedCenter += neighbours[i].position;
            }

            percievedCenter /= (neighbours.Length);
            return (percievedCenter - transform.position) / steps * weight;
        }

        Vector3 Seperation(Transform[] neighbours,float steps, float weight)
        {
            Vector3 c = Vector3.zero;
                
            for (int i = 0; i < neighbours.Length; i++)
            {
                
                var neighborPos = neighbours[i].position;
                var dist = (neighborPos - transform.position).magnitude;
                if(dist==0) continue; 
                c += Vector3.Normalize(transform.position - neighborPos) / dist * steps;
            }

            return c * weight;
        }

        Vector3 Alignment(Transform[] neighbours,float weight)
        {
            Vector3 pv = Vector3.zero;
            for (var i = 0; i < neighbours.Length; ++i)
            {
                pv += neighbours[i].GetComponent<BoidAgent>().Velocity;
            }
            pv /= (neighbours.Length);
            return Vector3.Normalize(pv - Velocity) * weight;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,_boidRules.radius);
        }
    }
}