using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : FSMAction
{
    private string textToShow;
    private float duration;
    private float cachedDuration;
    private string finishEvent;
    private EnemyAstar theEnemy;
    private Transform target;
    public IdleAction(FSMState owner) : base(owner)
    {
    }
    public void Init(Transform target, string textToShow, float duration, EnemyAstar tE, string finishEvent = null)
    {
        this.textToShow = textToShow;
        this.duration = duration;
        this.cachedDuration = duration;
        this.finishEvent = finishEvent;
        this.theEnemy = tE;
        this.target = target;
    }
    public override void OnEnter()
    {
        
        //Debug.Log("ToIdle");
        if(theEnemy != null)
            theEnemy.GetAnim().SetFloat("speed", 0);
    }

    public override void OnUpdate()
    {
        duration -= Time.deltaTime;
        if (theEnemy != null)
        {
            
            
            if (duration <= 0 && Vector2.Distance(theEnemy.gameObject.transform.position, target.position) > 1)
            {
                Finish();
                return;
            }
        }
       // Debug.Log(textToShow);
    }

    public override void OnExit()
    {

    }

    public void Finish()
    {
        
        if (!string.IsNullOrEmpty(finishEvent) && finishEvent == "ToPatrol")
        {

                    Debug.Log(theEnemy);
                        //Debug.Log(theMachine.testNum.ToString());
                        theEnemy.stopWandering = false;
                        theEnemy.followTargetGo = true;
                        theEnemy.target = target;
                        if (!theEnemy.stopWandering && theEnemy.followTargetGo)
                            theEnemy.GetAnim().SetFloat("speed", 1);
            
        }
        if (!string.IsNullOrEmpty(finishEvent))
        {
            GetOwner().SendEvent(finishEvent);
        }
        
        duration = cachedDuration;
    }
}
