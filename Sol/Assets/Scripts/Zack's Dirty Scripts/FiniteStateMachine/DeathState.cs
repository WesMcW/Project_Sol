using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : FSMAction
{
    private string textToShow;
    private float duration;
    private float cachedDuration;
    private string finishEvent;
    EnemyAstar theEnemy;

    public DeathState(FSMState owner) : base(owner)
    {
    }

    public void Init(float duration, EnemyAstar tE, string finishEvent = null)
    {
        this.duration = duration;
        this.cachedDuration = duration;
        this.finishEvent = finishEvent;
        this.theEnemy = tE;
        
    }
    public override void OnEnter()
    {
        if (duration <= 0)
        {
            Finish();
            return;
        }
    }

    public override void OnUpdate()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            
            theEnemy.gameObject.GetComponent<NpcStats>().DestroyMe();
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
        duration = cachedDuration;
    }
}
