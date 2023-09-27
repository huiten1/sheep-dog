using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class AreaSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform areaTf;
        [SerializeField] private float stepLength;
        private void Start()
        {
            for (float x = -areaTf.transform.localScale.x/2f; x <= areaTf.transform.localScale.x/2f; x+=stepLength)
            {
                for (float y = -areaTf.transform.localScale.z/2f; y <= areaTf.transform.localScale.z/2f; y+=stepLength)
                {
                    var randomOffset = Random.insideUnitCircle;
                    Vector3 pos = areaTf.transform.position + new Vector3(x +randomOffset.x  , 0, y + randomOffset.y);

                    Instantiate(prefab, pos, quaternion.identity);
                }        
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(areaTf.position,areaTf.localScale);
        }
    }
}