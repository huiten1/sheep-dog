using System;
using UnityEngine.Events;

namespace Indicator
{
    public interface IIndicator<T>
    {
        T Value { get;  }
        event Action OnValueChanged;
    }
}