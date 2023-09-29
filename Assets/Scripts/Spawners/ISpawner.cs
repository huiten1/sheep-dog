using System;
using UnityEngine;

namespace Spawners
{
    public interface  ISpawner
    {
        T[] Spawn<T>(T prefab,Action<T> spawnAction=null) where T : MonoBehaviour;
    }
}