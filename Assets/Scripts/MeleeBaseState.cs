using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{

    public float duration;
    protected Animator animator;
    protected bool shouldCombo;
    protected int attackIndex;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();

    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown("X") == true)
            shouldCombo = true;

    }

    public override void OnExit()
    {
        base.OnExit();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
