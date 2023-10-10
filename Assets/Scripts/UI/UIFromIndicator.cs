using System;
using Indicator;
using UnityEngine;

namespace UI
{
    public abstract class UIFromIndicator<T> : MonoBehaviour
    {
        [SerializeField] protected GameObject indicatorObject;
        protected IIndicator<T> Indicator;

        protected virtual void Start()
        {
            Indicator = indicatorObject.GetComponent<IIndicator<T>>();
            Indicator.OnValueChanged += UpdateUI;
        }
        
        private void OnValidate()
        {
            if(!indicatorObject) return;
            if (indicatorObject.GetComponent<IIndicator<T>>() == null)
            {
                indicatorObject = null;
            }
        }
        protected abstract void UpdateUI();
    }
}