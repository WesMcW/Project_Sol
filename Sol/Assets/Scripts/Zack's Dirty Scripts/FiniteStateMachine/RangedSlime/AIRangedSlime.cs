using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rangedSlimey
{
    public class AIRangedSlime : MonoBehaviour
    {
        private FiniteStateMachine fsm;
        private FSMState PatrolState;
        private FSMState IdleState;
        private FSMState ScanState;
        private WanderAction PatrolAction;
        private IdleAction IdleAction;
        private ScanningAction ScanAction;
        private FSMState deathState;
        private DeathState DeathAction;
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
            deathState = fsm.AddState("DeathState");
            DeathAction = new DeathState(deathState);
            //This adds the actions to the state and add state to it's transition map
            PatrolState.AddAction(PatrolAction);
            IdleState.AddAction(IdleAction);

            PatrolState.AddTransition("ToIdle", IdleState);
            IdleState.AddTransition("ToPatrol", PatrolState);
            ScanState.AddTransition("ToScanning", ScanState);
            deathState.AddTransition("ToDeath", deathState);
            PatrolAction.Init(target, 3.0f, gameObject.GetComponent<EnemyAstar>(), "ToIdle");
            IdleAction.Init(target, "AI on Idle", 3.0f, gameObject.GetComponent<EnemyAstar>(), "ToPatrol");
            DeathAction.Init(3.0f, gameObject.GetComponent<EnemyAstar>());
            ScanAction.Init(3.0f);
            fsm.StartMachine("IdleState");

        }

        private void Update()
        {
            fsm.Update();
        }
    }
}
