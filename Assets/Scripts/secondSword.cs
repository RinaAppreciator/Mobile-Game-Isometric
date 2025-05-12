using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondSword : MonoBehaviour
{
    public Animator animator;
    public bool shaking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("found something");
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            Slowdown();
            Shake(2);
            Debug.Log("found enemy by sword ");
            enemy.Fix();
        }

    }



    public void Slowdown()
    {
        animator.speed = 0;

        StartCoroutine(RestoreSpeedCoroutine());
    }

    IEnumerator RestoreSpeedCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        animator.speed = 1f;


    }


    public void Shake(int shakeCount)
    {
        StartCoroutine(ShakeRoutine(shakeCount));
    }

    private IEnumerator ShakeRoutine(int shakeCount)
    {
        GetComponent<Rigidbody2D>().simulated = false;
        shaking = true;
        Vector2 originalPosition = GetComponent<Rigidbody2D>().position;
        float shakeSpeed = 0.1f; // Adjust for how fast the shake happens

        for (int i = 0; i < shakeCount; i++)
        {
            // Move slightly right
            GetComponent<Rigidbody2D>().MovePosition(originalPosition + Vector2.right * 0.2f);
            yield return new WaitForSeconds(shakeSpeed);

            // Move slightly left
            GetComponent<Rigidbody2D>().MovePosition(originalPosition + Vector2.left * 0.2f);
            yield return new WaitForSeconds(shakeSpeed);
            shakeSpeed -= 0.02f;
        }

        // Return to the original position
        //rigidbody2D.MovePosition(originalPosition);
        GetComponent<Rigidbody2D>().simulated = true;
        shaking = false;
    }

}
