using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBehaviour : StateMachineBehaviour
{
    public Quest quest;
    PlayerQuest player;
    Inventory inv;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = PlayerQuest.instance;
        inv = Inventory.inventory;
        quest.isActive = true;
        player.activeQuests++;
        player.quest.Add(quest);

        //Completes the quest if you have the items required for it
        for (int i = 0; i < player.activeQuests; i++)
        {
            if (inv.HasItems(player.quest[i].goal.requiredID, player.quest[i].goal.requiredAmount))
            {
                player.quest[i].goal.currentAmount = player.quest[i].goal.requiredAmount;

                player.quest[i].Complete();
            }
            else
            {
                player.quest[i].goal.currentAmount = inv.itemCount(player.quest[i].goal.requiredID);
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
