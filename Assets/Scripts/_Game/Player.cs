using System;
using Flocking.Behaviours;
using UnityEngine;


    public class Player : MonoBehaviour
    {
        [SerializeField] private GetAwayFromRadius stayInRadiusBehaviour;

        private void Update()
        {
            stayInRadiusBehaviour.center = transform.position;
        }
    }
