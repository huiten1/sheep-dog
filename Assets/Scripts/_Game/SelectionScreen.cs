using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game
{
    public class SelectionScreen : MonoBehaviour
    {
        [SerializeField] private GameObject[] selections;
        [Header("references")]
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Transform camTf;
        private int _currentSelection = 0;
        private Vector3 offset;
        public UnityEvent<GameObject> onSelected;
        private void Start()
        {
            offset = selections[_currentSelection].transform.position - camTf.position;
            
            leftButton.onClick.AddListener(GoLeft);
            rightButton.onClick.AddListener(GoRight);
            startButton.onClick.AddListener(()=>GameManager.Instance.StartGame());
            startButton.onClick.AddListener(()=>onSelected?.Invoke(selections[_currentSelection]));
            Refresh();
        }

        private void GoRight()
        {
            _currentSelection++;
            Refresh();
        }

        private void GoLeft()
        {
            _currentSelection--;
            Refresh();
        }

        private void Refresh()
        {
            camTf.DOMove(selections[_currentSelection].transform.position-offset,0.6f);
            leftButton.gameObject.SetActive(_currentSelection != 0);
            rightButton.gameObject.SetActive(_currentSelection != selections.Length-1);
        }
    }
}