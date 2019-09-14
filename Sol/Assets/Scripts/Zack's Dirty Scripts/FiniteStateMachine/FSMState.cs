﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState
{
	private List<FSMAction> actions;
	FiniteStateMachine owner;
	Dictionary<string, FSMState> transitionMap;
    private readonly string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    /// <summary>
    /// </summary>
    public FSMState(string name, FiniteStateMachine owner)
	{
		this.name = name;
		this.owner = owner;
		this.transitionMap = new Dictionary<string, FSMState>();
		this.actions = new List<FSMAction>();
	}

	/// <summary>
	/// Adds the transition.
	/// </summary>
	public void AddTransition(string id, FSMState destinationState)
	{
		if (transitionMap.ContainsKey(id))
		{
			Debug.LogError(string.Format("state {0} already contains transition for {1}", this.name, id));
			return;
		}

		transitionMap[id] = destinationState;
	}

	/// <summary>
	/// Gets the transition.
	/// </summary>
	public FSMState GetTransition(string eventId)
	{
		if (transitionMap.ContainsKey(eventId))
		{
			return transitionMap[eventId];
		}

		return null;
	}

	/// <summary>
	/// Adds the action.
	/// </summary>
	public void AddAction(FSMAction action)
	{
		if (actions.Contains(action))
		{
			Debug.LogWarning("This state already contains " + action);
			return;
		}

		if (action.GetOwner() != this)
		{
			Debug.LogWarning("This state doesn't own " + action);
		}

		actions.Add(action);
	}

	/// <summary>
	/// This gets the actions of this state
	/// </summary>
	/// <returns>The actions.</returns>
	public IEnumerable<FSMAction> GetActions()
	{
		return actions;
	}

	/// <summary>
	/// Sends the event.
	/// </summary>
	public void SendEvent(string eventId)
	{
		this.owner.SendEvent(eventId);
	}
	
}