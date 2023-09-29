using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class AreaSpawner : MonoBehaviour,ISpawner
    {
        [SerializeField] private Transform areaTf;
        [SerializeField] private float stepLength;
        private void Start()
        {
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(areaTf.position,areaTf.localScale);
        }

        public T[] Spawn<T>(T prefab,Action<T> spawnAction) where T : MonoBehaviour
        {
            List<T> spawnedObjects = new();
            for (float x = -areaTf.transform.localScale.x/2f; x <= areaTf.transform.localScale.x/2f; x+=stepLength)
            {
                for (float y = -areaTf.transform.localScale.z/2f; y <= areaTf.transform.localScale.z/2f; y+=stepLength)
                {
                    var randomOffset = Random.insideUnitCircle;
                    Vector3 pos = areaTf.transform.position + new Vector3(x +randomOffset.x  , 0, y + randomOffset.y);

                    var spawnedObject = Instantiate(prefab, pos, quaternion.identity);
                    spawnAction?.Invoke(spawnedObject);
                    spawnedObjects.Add(spawnedObject);
                }        
            }

            return spawnedObjects.ToArray();
        }
    }
}