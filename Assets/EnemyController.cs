using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public bool vertical;
    public bool horizontal;
    Rigidbody2D rigidbody2D;
    public ParticleSystem smokeEffect;
    public GameObject HitEffectPrefab;
    public AudioClip FixedClip;
    private float timerValue = 2f;
    private float remainingTimer;
    private float direction;
    private int directionNumber;
    bool broken = true;
    bool shaking = false;
    int shakenTimes;
    Vector2 position;
    Vector2 lastPosition;
    Animator animator;
    AudioSource audioSource;
    private character_controller player;

    // Start is called before the first frame update
    void Start()

    {
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

        if (!broken)
        {
            return;
        }

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

        character_controller player = other.gameObject.GetComponent<character_controller>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }

    }


    public void Fix()
    {
        Debug.Log("enemy fixed");
        StartCoroutine(HitStopForObject(0.1f));
        Shake(2);
        animator.SetTrigger("Fixed");
        broken = false;
        audioSource.Stop();
        smokeEffect.Stop();
        PlaySound(FixedClip);
        GameObject hitParticle = Instantiate(HitEffectPrefab, rigidbody2D.position, Quaternion.identity) ;
    }


    private IEnumerator HitStopForObject(float duration)
    {

        Time.timeScale = 0f; // Pause the game
        yield return new WaitForSecondsRealtime(duration); // Wait using real time (not affected by timeScale)
        Time.timeScale = 1f; // Resume the game
    }





    public void Shake(int shakeCount)
    {
        StartCoroutine(ShakeRoutine(shakeCount));
    }

    private IEnumerator ShakeRoutine(int shakeCount)
    {
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
        rigidbody2D.simulated = false;
        shaking = false;
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
