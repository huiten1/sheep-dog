using System;
using Indicator;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextIndicator : UIFromIndicator<string>
    {
        [SerializeField] private TMP_Text tmpText;
        protected override void UpdateUI()
        {
            tmpText.SetText(Indicator.Value);
        }
    }
}