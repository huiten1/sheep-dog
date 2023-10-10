using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class LevelCompleteScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button continueButton;
        private void Awake()
        {
            restartButton.onClick.AddListener(GameManager.Instance.LoadMain);
            continueButton.onClick.AddListener(GameManager.Instance.LoadMain);
        }
    }
}