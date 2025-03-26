using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwordController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(Vector2 launchDirection)
    {

       animator.SetFloat("Look X", launchDirection.x);
       animator.SetFloat("Look Y", launchDirection.y);
        animator.SetTrigger("Attack");
               
          
    }
    // pdate is called once per frame
    void Update()
    {
        
    }
}
