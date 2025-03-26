using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_controller : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;

    public InputAction talkAction;

    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    //health
    public int maxHealth = 5;
    int currentHealth;
    
    //speed
    public float speed = 3.0f;

    //invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    //visual effects
    public GameObject projectilePrefab;
    public GameObject HitParticlePrefab;

    AudioSource audioSource;
    public AudioClip HitClip;
    public AudioClip ShootClip;
    public AudioClip WalkClip;



    public int health {
        get
        {
            return currentHealth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        talkAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveDirection);

        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();

        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFriend();
        }

    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
            {
            if (isInvincible)
                {
                return;
                }
            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
            GameObject hitParticleObject = Instantiate(HitParticlePrefab, rigidbody2d.position, Quaternion.identity);
            PlaySound(HitClip);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIhandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    public void hit(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        PlaySound(HitClip);
    }


    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        projectile projectile = projectileObject.GetComponent<projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
        PlaySound(ShootClip);
    }

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                UIhandler.instance.DialogueVisibility();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
