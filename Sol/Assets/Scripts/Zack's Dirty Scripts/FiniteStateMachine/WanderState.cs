using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : FSMAction
{
    private float maxTimeInDir;
    private float TimeInDir;
    private Transform target;
    private string finishEvent;
    float polledTime;
    EnemyAstar theMachine;

    public WanderAction(FSMState owner) : base(owner)
    {
    }

    public void Init(Transform target, float duration, EnemyAstar t, string finishEvent = null)
    {
        this.target = target;
        this.TimeInDir = Random.Range(0, duration);
        this.maxTimeInDir = duration;
        this.finishEvent = finishEvent;
        this.polledTime = 0;
        this.theMachine = t;
    }
    public override void OnEnter()
    {
        theMachine.followTargetGo = true;
        if (TimeInDir <= 0)
        {
            Finish();
            return;
        }
    }
    public override void OnUpdate()
    {
        polledTime += Time.deltaTime;
        TimeInDir -= Time.deltaTime;

        if (TimeInDir <= 0)
        {
            Finish();
            return;
        }
    }
   
    private void Finish()
    {
        if (!string.IsNullOrEmpty(finishEvent))
        {
            GetOwner().SendEvent(finishEvent);
        }

        theMachine.followTargetGo = false;
        this.polledTime = 0;
        TimeInDir = Random.Range(0, maxTimeInDir);
        
    }

}
