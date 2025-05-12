using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColllectibleHealth : MonoBehaviour
{
    public int healNumber;
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {

        character_controller controller = other.GetComponent<character_controller>();

        if (controller != null && controller.maxHealth > controller.health)
        {
            controller.ChangeHealth(healNumber);
            controller.PlaySound(collectedClip);        
            Destroy(gameObject);
        }



    }
}
