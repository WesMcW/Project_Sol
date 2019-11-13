using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : Interactable
{
    [Header("Use the Item ID's for the Loot!")]
    [SerializeField] int[] possibleLoot;
    [Header("How much loot is in the container?")]
    [SerializeField] int lootAmount;

    public override void Interact()
    {
        for (int i = 0; i < lootAmount; i++)
        {
            GameObject tmp = Instantiate(ItemIDManager.instance.GetItem(possibleLoot[Random.Range(0, possibleLoot.Length - 1)]), this.transform.position, Quaternion.identity);
            
        }

        InteractionManager.IM.StopInteraction();
        Destroy(this.gameObject);
    }
   
}
