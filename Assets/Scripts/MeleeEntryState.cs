using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : State
{
    // Start is called before the first frame update
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        State nextState = (State)new PunchEntryState();
        stateMachine.SetNextState(nextState);
    }
}
