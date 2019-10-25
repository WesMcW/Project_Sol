using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCheck : StateMachineBehaviour
{
    PlayerQuest player;
    Inventory inv;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Get the instance of player quest & inventory
        player = PlayerQuest.instance;
        inv = Inventory.inventory;
        //Get the list of acceptable quest id's for the NPC.
        List<int> ids = animator.gameObject.GetComponent<NPC_Dialogue>().acceptableIDs;

        //Check the player's active quests
        if (player.activeQuests != 0)
        {
            for (int i = 0; i < player.activeQuests; i++)
            {
                if (player.quest[i].isActive)
                {
                    foreach (int id in ids)
                    {
                        if (id == player.quest[i].questID && player.quest[i].finished)
                        {
                            //Removes the items required for the quest
                            inv.RemoveItems(player.quest[i].goal.requiredID, player.quest[i].goal.requiredAmount);
                            player.quest[i].resetQuests();

                            //Adds quest to completed list
                            player.CurrentExperience += player.quest[i].experienceReward;
                            player.activeQuests--;
                            player.finishedQuests.Add(player.quest[i]);
                            player.gameObject.GetComponent<Journal>().removeButton(player.quest[i]);
                            player.quest.Remove(player.quest[i]);

                            animator.SetInteger("Progress", animator.GetInteger("Progress") + 1);
                            animator.SetTrigger("Qcomplete");
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            animator.SetTrigger("path1");
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
