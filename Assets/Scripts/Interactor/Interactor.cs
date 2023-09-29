using System;
using UnityEngine;

namespace Interactor
{
    public class Interactor : MonoBehaviour
    {
        private IInteractable _currentInteractable;
        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponent<IInteractable>();
            if (interactable == null) return;
            interactable.Interact(this);
            _currentInteractable = interactable;
        }

        private void OnTriggerExit(Collider other)
        {
            if(_currentInteractable==null) return;
            _currentInteractable.StopInteract(this);
            _currentInteractable = null;
        }
    }
}