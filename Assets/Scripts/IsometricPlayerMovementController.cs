using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class IsometricPlayerMovementController : MonoBehaviour
{
    public Vector2 inputVector;

    public inputManager RemotePlayerController;

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    SwordController swordController;
    public GameObject projectilePrefab;
    public Vector2 movement;
    bool isGrounded;
    bool isJumping;
    float jumpVelocity = 5f;
    float gravity = -9.8f;
    float verticalSpeed = 0f;
    public Animator moves;
    public int HP;

    Vector2 newPos;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        swordController = GetComponentInChildren<SwordController>();
        
    }


    public void Update()
    {
        inputVector = RemotePlayerController.position;
        if (RemotePlayerController.attackValue == 1 )
        {
            Debug.Log("attacking");
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }


    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;



        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");



        //Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        //inputVector = Vector2.ClampMagnitude(inputVector, 1);
        movement = inputVector * movementSpeed;
        newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);

    }


    public void Launch()
    {
        Vector2 launchDirection = isoRenderer.getAttackDirection();
        GameObject projectileObject = Instantiate(projectilePrefab, rbody.position + Vector2.up * 0.5f, Quaternion.identity);
        projectile projectile = projectileObject.GetComponent<projectile>();
        projectile.Launch(launchDirection, 300);
    }

    public void Attack()
    {
        Vector2 launchDirection = isoRenderer.getAttackDirection();
        swordController.Attack(launchDirection);
    }

    public void Slowdown()
    {
        moves.speed = 0;
        rbody.simulated = false;
        StartCoroutine(RestoreSpeedCoroutine());
    }

    IEnumerator RestoreSpeedCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        moves.speed = 1f;
        rbody.simulated = true;

    }

    public void TakeDamage()
    {
        HP -= 2;
    }


}
