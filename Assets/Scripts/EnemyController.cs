using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public bool vertical;
    public bool horizontal;
    Rigidbody2D rigidbody2D;
    public GameObject HitEffectPrefab;
    public AudioClip FixedClip;
    private float timerValue = 2f;
    private float remainingTimer;
    private float direction;
    private int directionNumber;
   
    bool shaking = false;
    int shakenTimes;
    Vector2 position;
    Vector2 lastPosition;
    Animator animator;
    AudioSource audioSource;
    private character_controller player;
    public int HP;

    // Start is called before the first frame update
    void Start()

    {
        HP = 10;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        horizontal = true;
        vertical = false;
        direction = 1;
        remainingTimer = timerValue;

        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<character_controller>();

    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0)
        {
            StartCoroutine(Death());
        }

        remainingTimer -= Time.deltaTime;
            if (remainingTimer < 0)
            {
                direction = -direction;
                remainingTimer = timerValue;
                directionNumber = Random.Range(1, 3);
            }

        if ( directionNumber == 1 )
        {
            horizontal = true;
            vertical = false;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        else if ( directionNumber == 2)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            vertical = true;
            horizontal = false;
        }
        
    }



    private void FixedUpdate()
    {

     

        position = rigidbody2D.position;
        if (vertical)
        {
            position.y = position.y + Speed * direction * Time.deltaTime;

        }
        else if ( horizontal)
        {
            position.x = position.x + Speed * direction * Time.deltaTime;

        }

        //position = position + new Vector2(2,4) * Speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);

        if (shaking == false)
        {

            lastPosition = position;
        }
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("player enter");

        IsometricPlayerMovementController player = other.gameObject.GetComponent<IsometricPlayerMovementController>();

        if (player != null)
        {
            Debug.Log("player hit");
            player.TakeDamage();
        }

    }


    public void Fix()
    {
        Debug.Log("hit enemy ");

      
            HP -= 4;
            Debug.Log("enemy fixed");
            StartCoroutine(HitStopForObject(0.1f));
            Shake(2);
            //animator.SetTrigger("Fixed");
            //broken = false;
            audioSource.Stop();
            
            PlaySound(FixedClip);
            GameObject hitParticle = Instantiate(HitEffectPrefab, rigidbody2D.position, Quaternion.identity);
        
    }


    public IEnumerator  Death()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private IEnumerator HitStopForObject(float duration)
    {

        animator.speed = 0f; // Pause the game
        yield return new WaitForSecondsRealtime(duration); // Wait using real time (not affected by timeScale)
        animator.speed = 1f; // Resume the game
    }





    public void Shake(int shakeCount)
    {
        StartCoroutine(ShakeRoutine(shakeCount));
    }

    private IEnumerator ShakeRoutine(int shakeCount)
    {
        rigidbody2D.simulated = false;
        shaking = true;
        Vector2 originalPosition = rigidbody2D.position;
        float shakeSpeed = 0.1f; // Adjust for how fast the shake happens

        for (int i = 0; i < shakeCount; i++)
        {
            // Move slightly right
            rigidbody2D.MovePosition(originalPosition + Vector2.right * 0.2f);
            yield return new WaitForSeconds(shakeSpeed);

            // Move slightly left
            rigidbody2D.MovePosition(originalPosition + Vector2.left * 0.2f);
            yield return new WaitForSeconds(shakeSpeed);
            shakeSpeed -= 0.02f;
        }

        // Return to the original position
        //rigidbody2D.MovePosition(originalPosition);
        rigidbody2D.simulated = true;
        shaking = false;
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
