using System;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter2D player");
            PlayerController player = other.GetComponent<PlayerController>();
            player.Speed = 2;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit2D player");
            PlayerController player = other.GetComponent<PlayerController>();
            player.Speed = 5;
        }
    }
}
