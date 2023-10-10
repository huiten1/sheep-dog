using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarSlider : UIFromIndicator<float>
    {
       [SerializeField] private Slider slider;
       protected override void Start()
       {
           base.Start();
           slider.minValue = 0;
           slider.maxValue = 1;
       }

       protected override void UpdateUI()
       {
           slider.value = Indicator.Value;
       }
    }
}