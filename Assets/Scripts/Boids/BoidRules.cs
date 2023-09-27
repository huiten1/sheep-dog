using UnityEngine;

namespace Boids
{
    [CreateAssetMenu(menuName = "Data/Create BoidRule", fileName = "BoidRule", order = 0)]
    public class BoidRules:ScriptableObject
    {
        [SerializeField] public LayerMask mask;

        [SerializeField] public float radius;
        [SerializeField] public float maxSpeed;

        [Space(25)]
        [Header("Cohseion")]
        [Range(0,100f)]
         public float cohesionWeight;

         public float cohesionSteps;

        [Space(25)]
        [Header("Seperation")]
        [Range(0,100f)]
         public float seperationWeight;

         public float seperationSteps;

        [Space(25)]
        [Header("Alignment")]
        [Range(0,100f)]
         public float alignmentWeight;

         [Header("Flee")] public float fleeWeight;
         [Header("Seek")] public float seekWeight;
         private void OnValidate()
        {
            cohesionSteps = Mathf.Clamp(cohesionSteps, 0.01f, float.PositiveInfinity);
            seperationSteps = Mathf.Clamp(seperationSteps, 0.01f, float.PositiveInfinity);

        }
    }
}