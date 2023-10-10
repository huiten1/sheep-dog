
using System;
using System.Threading.Tasks;
using Flocking;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace _Game.AI
{
    public class SheepAI : MonoBehaviour,IKillable
    {
        [SerializeField] private Animator animator;
        private static readonly int Death = Animator.StringToHash("die");
        public int price = 5;
        private bool _enteredGate;

        private void Start()
        {
            if (name.ToLower().Contains("gold"))
            {
                price = GameManager.Instance.GameData.goldenSheepPrice;
            }
            else
            {
                price = GameManager.Instance.GameData.sheepPrice;
            }
        }

        public async void OnEnterGate()
        {
            if(_enteredGate) return;
            _enteredGate = true;
        }
        public void Die()
        {
            GetComponent<FlockAgent>().RemoveFromFlock();
            foreach (var componentsInChild in GetComponentsInChildren<Collider>())
            {
                componentsInChild.enabled = false;
            }
            animator.SetTrigger(Death);
        }
    }
}