using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int HealNumber;
    void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("Object that entered the trigger: " + other);
        character_controller controller = other.GetComponent<character_controller>();

        if (controller != null)
        {
            controller.ChangeHealth(HealNumber);
        }



    }

}
