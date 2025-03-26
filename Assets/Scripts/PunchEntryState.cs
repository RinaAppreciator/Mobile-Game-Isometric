using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEntryState : MeleeBaseState
{
    // Start is called before the first frame update
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackIndex = 1;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {

        if(fixedtime >= duration)
        {
            if (shouldCombo)
                stateMachine.SetNextState(new PunchMiddleState());
            else
                stateMachine.SetNextStateToMain();
        }
        
    }




}
