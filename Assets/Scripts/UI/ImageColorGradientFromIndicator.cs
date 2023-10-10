using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ImageColorGradientFromIndicator : UIFromIndicator<float>
    {
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image image;
        protected override void UpdateUI()
        {
            image.color = gradient.Evaluate(Indicator.Value);
        }
    }
}