using UnityEngine;

namespace Flocking
{
    public abstract class FilteredFlockBehaviour : FlockBehaviour
    {
        public ContextFilter filter;
    }
}