﻿using System.Collections.Generic;
using UnityEngine;


namespace Flocking
{
    public abstract class FlockBehaviour : ScriptableObject
    {
        public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    }


}