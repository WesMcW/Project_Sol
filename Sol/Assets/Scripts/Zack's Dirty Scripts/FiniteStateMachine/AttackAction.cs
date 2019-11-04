using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : FSMAction
{
    private string textToShow;
    private float duration;
    private float cachedDuration;
    private string finishEvent;

    public AttackAction(FSMState owner) : base(owner)
    {
    }

    public void Init(string textToShow, float duration, string finishEvent)
    {
        this.textToShow = textToShow;
        this.duration = duration;
        this.cachedDuration = duration;
        this.finishEvent = finishEvent;
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
        duration = cachedDuration;
    }
}
