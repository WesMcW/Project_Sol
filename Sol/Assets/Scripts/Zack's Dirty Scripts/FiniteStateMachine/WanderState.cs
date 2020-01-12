using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : FSMAction
{
    private float maxTimeInDir;
    private float TimeInDir;
    private Transform target;
    private string finishEvent;
    private float polledTime;
    private EnemyAstar theMachine;

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
        theMachine = t;
    }
    public override void OnEnter()
    {
        //Debug.Log("ToPatrol");
        if(target != null)
            if (TimeInDir <= 0 || Vector2.Distance(target.position, theMachine.transform.position) < 1)
            {
                Finish();
                return;
            }
        /***
        if (theMachine != null)
        {
            //if (Vector2.Distance(theMachine.gameObject.transform.position, target.position) > 1)
            //{

                //Debug.Log(theMachine.testNum.ToString());
                theMachine.stopWandering = false;
                theMachine.followTargetGo = true;
                theMachine.target = target;
                theMachine.GetAnim().SetFloat("speed", 1);
           // }
        }
        ***/
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
       // Debug.Log("OnPatrol");
            if (TimeInDir <= 0 || Vector2.Distance(target.position, theMachine.transform.position) < 1)
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
        if (theMachine != null)
        {
            theMachine.target = null;
            theMachine.followTargetGo = false;
        }
        this.polledTime = 0;
        TimeInDir = Random.Range(0, maxTimeInDir);
        
    }

}
