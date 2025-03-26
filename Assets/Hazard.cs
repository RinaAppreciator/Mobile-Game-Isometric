using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int DamageNumber;
    void OnTriggerStay2D (Collider2D other)
    {

    
        character_controller controller = other.GetComponent<character_controller>();

        if (controller != null)
        {
            controller.ChangeHealth(DamageNumber);
        }



    }
}
