using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningAction : FSMAction
{
    private float TimeInDir;
    private float maxTimeInDir;
    private Transform target;
    private string finishEvent;
    private float polledTime;
    private EnemyAstar theMachine;
    public ScanningAction(FSMState owner) : base(owner)
    {
    }
    public void Init(float duration, string finishEvent = null)
    {
        
    }
    public override void OnEnter()
    {
        if (TimeInDir <= 0)
        {
            Finish();
            return;
        }
    }

    public override void OnUpdate()
    {
        TimeInDir -= Time.deltaTime;

        if (TimeInDir <= 0)
        {
            Finish();
            return;
        }

        Debug.Log(textToShow);
    }

    public override void OnExit()
    {

    }

    public void Finish()
    {
        if (!string.IsNullOrEmpty(finishEvent))
        {
            GetOwner().SendEvent(finishEvent);
        }
        TimeInDir = cachedDuration;
    }
}
