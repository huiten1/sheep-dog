using System;
using System.Collections;
using Flocking;
using Interactor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace _Game.AgentInteractions
{
    public class EnterGateInteraction : MonoBehaviour,IInteractable
    {
        public UnityEvent onEnterGate;
        private bool _entered;
        private bool isOutside;
        private FlockAgent _flockAgent;
        
        private void Start()
        {
            _flockAgent = GetComponent<FlockAgent>();
        }

        public void Interact(Interactor.Interactor interactor)
        {
            if(_entered) return;
            if (interactor.CompareTag("gate"))
            {

              
                onEnterGate?.Invoke();
                var interact = interactor;
                var flocks = interact.GetComponents<Flock>();
                _flockAgent.RemoveFromFlock();
                flocks[Random.Range(0,flocks.Length)].AddToFlock(_flockAgent);
            }
        }



        public void StopInteract(Interactor.Interactor interactor)
        {
            _entered = false;
        }
    }
}