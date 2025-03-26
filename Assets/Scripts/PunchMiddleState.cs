using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMiddleState : MeleeBaseState
{
    // Start is called before the first frame update
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackIndex = 2;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {

        if (fixedtime >= duration)
        {
            if (shouldCombo)
                stateMachine.SetNextState(new PunchFinisherState());
            else
                stateMachine.SetNextStateToMain();
        }

    }

}
