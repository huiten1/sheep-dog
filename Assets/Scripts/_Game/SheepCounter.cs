using System;
using System.Linq;
using _Game.AI;
using _Game.UI;
using Flocking;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Game
{
    public class SheepCounter : MonoBehaviour
    {
        [SerializeField] public Flock mainFlock;
        public int enteredSheepCount = 0;
        public int StartCount => mainFlock.startFlockCount;
         public int total = 0;
         [SerializeField] private float goldSheepTextScale = 1f;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer!= LayerMask.NameToLayer("Boid")) return;

            enteredSheepCount++;

            enteredSheepCount = Mathf.Clamp(enteredSheepCount, 0, mainFlock.startFlockCount);

        
            int price = other.GetComponent<SheepAI>().price;
            total += price;
            var floatingText = GameManager.Instance.FloatingTextPool.Get();
            
            floatingText.transform.position = other.transform.position + Vector3.up*5;
            var tmpText = floatingText.GetComponent<TMP_Text>();
            if(other.name.ToLower().Contains("gold"))
            {
                floatingText.transform.localScale = Vector3.one * goldSheepTextScale;
            }
            else
            {
                floatingText.transform.localScale = Vector3.one;
            }
            
            floatingText.GetComponent<UIMoneyFx>().offset = new Vector3(Random.Range(-5,5), 0, Random.Range(-15,-20));
            tmpText.GetComponent<TMP_Text>().SetText($"+${price}");
            
            if (enteredSheepCount == mainFlock.startFlockCount)
            {
                GameManager.Instance.Complete();
            }
        }
    }
}