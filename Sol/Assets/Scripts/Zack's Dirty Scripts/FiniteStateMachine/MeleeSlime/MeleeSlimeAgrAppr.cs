using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSlimeAgrAppr : FSMAction
{
    private float maxTimeInDir;
    private float slimeRange;
    private float TimeInDir;
    private Transform target;
    private string finishEvent;
    private float polledTime;
    private EnemyAstar theMachine;

    public MeleeSlimeAgrAppr(FSMState owner) : base(owner)
    {
    }

    public void Init(Transform target, float duration, EnemyAstar t, float slimeRange = 1f, string finishEvent = null)
    {
        this.target = target;;
        this.finishEvent = finishEvent;
        theMachine = t;
        this.slimeRange = slimeRange;
    }
    public override void OnEnter()
    {
        if (target != null)
            if (Vector2.Distance(target.position, theMachine.transform.position) < slimeRange)
            {
                Finish();
                return;
            }
    }
    public override void OnUpdate()
    {
        //Sends creature to its death state
        if (theMachine.gameObject.GetComponent<NpcStats>().GetHealth() <= 0)
        {
            finishEvent = "ToDeath";
            Finish();
        }

        polledTime += Time.deltaTime;
        TimeInDir -= Time.deltaTime;

        if (Vector2.Distance(target.position, theMachine.transform.position) <= slimeRange)
        {
            finishEvent = "ToAttack";
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
        if (theMachine != null)
        {
            theMachine.followTargetGo = false;
        }
    }
}
