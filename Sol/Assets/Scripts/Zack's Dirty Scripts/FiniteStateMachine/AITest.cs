using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    private FiniteStateMachine fsm;
    private FSMState PatrolState;
    private FSMState IdleState;
    private FSMState ScanState;
    private WanderAction PatrolAction;
    private IdleAction IdleAction;
    private ScanningAction ScanAction;
    [SerializeField]
    Transform target;
    EnemyAstar thelocation;
    private void Start()
    {
        fsm = new FiniteStateMachine("AITest FSM");
        IdleState = fsm.AddState("IdleState");
        ScanState = fsm.AddState("ScanState");
        PatrolState = fsm.AddState("WanderState");
        PatrolAction = new WanderAction(PatrolState);
        IdleAction = new IdleAction(IdleState);
        ScanAction = new ScanningAction(ScanState);
        //This adds the actions to the state and add state to it's transition map
        PatrolState.AddAction(PatrolAction);
        IdleState.AddAction(IdleAction);

        PatrolState.AddTransition("ToIdle", IdleState);
        IdleState.AddTransition("ToPatrol", PatrolState);
        ScanState.AddTransition("ToScanning", ScanState);
        PatrolAction.Init(target, 3.0f, gameObject.GetComponent<EnemyAstar>(), "ToIdle");
        IdleAction.Init(target, "AI on Idle", 3.0f, gameObject.GetComponent<EnemyAstar>(), "ToPatrol");
        ScanAction.Init(3.0f);
        fsm.StartMachine("IdleState");
    }
    
private void Update()
    {
        fsm.Update();
    }
}
