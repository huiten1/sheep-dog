using UnityEngine;
using UnityEngine.Pool;

namespace Utils
{
    public class ReturnToPool : MonoBehaviour
    {
        public IObjectPool<GameObject> pool;
        public void Release()
        {
            pool.Release(gameObject);
        }
    }
}