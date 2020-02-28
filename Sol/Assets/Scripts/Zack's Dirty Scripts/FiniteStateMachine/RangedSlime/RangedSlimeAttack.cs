using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSlimeAttack : FSMAction
{
    private float slimeRange;
    private Transform target;
    private string finishEvent;
    private EnemyAstar theMachine;
    private GameObject bullet;
    public RangedSlimeAttack(FSMState owner) : base(owner)
    {
    }

    public void Init(Transform target, float duration, EnemyAstar t, GameObject bullet, float slimeRange = 8f, string finishEvent = null)
    {
        this.target = target;
        this.finishEvent = finishEvent;
        theMachine = t;
        this.slimeRange = slimeRange;
        this.bullet = bullet;
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
