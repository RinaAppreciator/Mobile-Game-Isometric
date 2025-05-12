using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class inputManager : MonoBehaviour
{
    public FixedJoystick joystick;
    private PlayerInput playerInput;
    public Vector2 position;
    public float attackValue;
    private InputAction movementTouch;
    private InputAction actionTouch;

    private Vector2 currentDirection = Vector2.zero;
    private bool isTouchHeld = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        movementTouch = playerInput.actions.FindAction("Movement");
        actionTouch = playerInput.actions.FindAction("Action");
    }



    public void TouchStarted()
    {
        attackValue = 1f;  // or true if using a bool
        Debug.Log("Attack started");
        Invoke(nameof(ResetAttack), 0.01f);

    }

    private  void ResetAttack()
    {
        attackValue = 0;

    }



    private void Update()
    {


        position = joystick.Direction;

    }
 

}
