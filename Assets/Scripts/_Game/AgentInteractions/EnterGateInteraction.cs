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
        private Vector3 outsideDirection;
        private void Start()
        {
            _flockAgent = GetComponent<FlockAgent>();
        }

        public void Interact(Interactor.Interactor interactor)
        {
            if(_entered) return;
            if (interactor.CompareTag("gate"))
            {
                outsideDirection = interactor.transform.position - transform.position;
                outsideDirection.Normalize();
                
                _entered = true;
                onEnterGate?.Invoke();
                var interact = interactor;
                var flocks = interact.GetComponents<Flock>();
                        
                flocks[Random.Range(0,flocks.Length)].AddToFlock(_flockAgent);
                //StartCoroutine(GoOutsideGate(() =>
                  //  interact.GetComponent<Flock>().AddToFlock(_flockAgent)));

            }
        }

        private IEnumerator GoOutsideGate(System.Action onComplete=null)
        {
            for (float t = 0; t < 3f; t+=Time.deltaTime)
            {
                _flockAgent.Move(outsideDirection*5);
                yield return null;
            }
            
            onComplete?.Invoke();
        }

        public void StopInteract(Interactor.Interactor interactor)
        {
            _entered = false;
        }
    }
}