using System.Collections;
 using TMPro;
 using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
 public class SlotMachine : MonoBehaviour
 {
     [SerializeField] private TextMeshProUGUI[] _slots;
 
     
 
     private bool _spinValues = false;
     
     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.L)) // Check for 'l' key press
         {
             _spinValues = true;
             for (int i = 0; i < _slots.Length; i++)
             {
                 StartCoroutine(SpinSlotsForTime(2.0f + i, _slots[i])); // Start spinning for 2 seconds
             }
         }
 
         if(_spinValues){
             foreach (TextMeshProUGUI slotText in _slots)
             {
                 slotText.text = "" + Random.Range(0, 9); // Change slot text
             }
         }
     }
     
     
     
     private IEnumerator SpinSlotsForTime(float spinTime, TextMeshProUGUI slotText)
     {
         yield return new WaitForSeconds(spinTime);

         slotText.text = "" + Random.Range(0, 9); // Change slot text

         // Find the index of the slotText
         int index = Array.IndexOf(_slots, slotText);

         // Stop the next slot after a delay
         if (index < _slots.Length - 1)
         {
             yield return new WaitForSeconds(0.1f); // Introduce delay before stopping next slot
             StartCoroutine(SpinSlotsForTime(0.1f, _slots[index + 1])); // Spin the next slot
         }
         else
         {
             _spinValues = false; // All slots have stopped spinning
         }
     }

 }