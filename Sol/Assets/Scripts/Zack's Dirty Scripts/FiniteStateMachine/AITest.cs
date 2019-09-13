using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    private FiniteStateMachine fsm;
    private FSMState PatrolState;
    private FSMState IdleState;
    private TextAction PatrolAction;
    private TextAction IdleAction;
    private void Start()
    {
        fsm = new FiniteStateMachine("AITest FSM");
        IdleState = fsm.AddState("IdleState");
        PatrolState = fsm.AddState("PatrolState");
        PatrolAction = new TextAction(PatrolState);
        IdleAction = new TextAction(IdleState);
        //This adds the actions to the state and add state to it's transition map
        PatrolState.AddAction(PatrolAction);
        IdleState.AddAction(IdleAction);

        PatrolState.AddTransition("ToIdle", IdleState);
        IdleState.AddTransition("ToPatrol", PatrolState);
        PatrolAction.Init("AI on patrol", 3.0f, "ToIdle");
        IdleAction.Init("AI on Idle", 2.0f, "ToPatrol");
        fsm.StartMachine("IdleState");
    }
    
private void Update()
    {
        fsm.Update();
    }
}
