using UnityEngine;
using Unity.Collections;
using System.Collections;

public class hitbox : MonoBehaviour
{

    public GameObject player;
    public IsometricPlayerMovementController playerScript;
    
    public Hurt selfHurtBox;
    public Animator anim;
  
    public float damage;
    public LayerMask layerMask;

    public AudioClip impactHit;
    public AudioClip soundHit;

    public void Start()
    {

    }





    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("player enter");
        Debug.Log(other.name);

        IsometricPlayerMovementController player = other.gameObject.transform.root.GetComponent<IsometricPlayerMovementController>();

        if (player != null)
        {
            Debug.Log("player hit");
            player.TakeDamage();
        }

    }






}


