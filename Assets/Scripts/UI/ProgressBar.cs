using System;
using System.Collections.Generic;
using Indicator;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : UIFromIndicator<float>
    {
        [Space(25)]
        [SerializeField] private Image fillImage;
        [SerializeField] private Type fillType;
        [SerializeField] private bool isReverse;

        public enum Type
        {
            Rect,
            Fill,
        }

        private readonly Dictionary<Type, System.Action<Image, float>> _imageUpdateMethods =
            new()
            {
                { Type.Fill ,ImageFill},
                { Type.Rect ,RectFill},
            };


        protected override void UpdateUI()
        {
            float value = isReverse? 1-Indicator.Value :Indicator.Value;
            _imageUpdateMethods[fillType](fillImage,value);
        }
        
        static void RectFill(Image image,float val)
        {
            var maxSize= ((RectTransform)image.rectTransform.parent).sizeDelta.x;
            image.rectTransform.sizeDelta = new Vector2(maxSize * val, image.rectTransform.sizeDelta.y);
        }
        static void ImageFill(Image image,float val)
        {
            image.fillAmount = val;
        }
    }
}